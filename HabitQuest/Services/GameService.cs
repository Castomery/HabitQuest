using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Data;
using HabitQuest.Enums;
using HabitQuest.Interfaces;
using HabitQuest.Models;

namespace HabitQuest.Services
{
    public class GameService : IGameService
    {
        private readonly IDatabaseService _db;
        private readonly IHabitService _habitService;

        public GameService(IDatabaseService db, IHabitService habitService)
        {
            _db = db;
            _habitService = habitService;
        }

        public Task<UserProfile> GetProfileAsync() =>
        _db.GetProfileAsync();

        public async Task<(int xpEarned, bool leveledUp, List<Badge> newBadges)> CompleteHabitAsync(Habit habit)
        {
            var profile = await _db.GetProfileAsync();
            var levelBefore = profile.Level;

            await _db.SaveLogAsync(new HabitLog
            {
                HabitId = habit.Id,
                XpEarned = habit.XpReward,
                CompletedAt = DateTime.Now
            });


            profile.TotalXp += habit.XpReward;

            var streakUpdated = await UpdateStreakAsync(profile);

            await _db.SaveProfileAsync(profile);

            var leveledUp = profile.Level > levelBefore;

            var newBadges = new List<Badge>();
            newBadges.AddRange(await CheckBadgesAsync(profile, BadgeTrigger.OnHabitCompleted));

            if (streakUpdated)
                newBadges.AddRange(await CheckBadgesAsync(profile, BadgeTrigger.OnStreakUpdated));

            if (leveledUp)
                newBadges.AddRange(await CheckBadgesAsync(profile, BadgeTrigger.OnLevelUp));

            return (habit.XpReward, leveledUp, newBadges);
        }



        private async Task<bool> UpdateStreakAsync(UserProfile profile)
        {
            var today = DateTime.Today;
            var lastDate = profile.LastCompletedDate.Date;

            if (lastDate == today)
                return false;

            if (lastDate == today.AddDays(-1))
                profile.CurrentStreak++;
            else
                profile.CurrentStreak = 1;

            if (profile.CurrentStreak > profile.LongestStreak)
                profile.LongestStreak = profile.CurrentStreak;

            profile.LastCompletedDate = today;
            return true;
        }

        private async Task<List<Badge>> CheckBadgesAsync(UserProfile profile,BadgeTrigger trigger)
        {
            var earnedKeys = (await _db.GetEarnedBadgesAsync())
                .Select(b => b.BadgeKey)
                .ToHashSet();

            var candidates = BadgeDefinitions.All
                .Where(b => b.Trigger == trigger && !earnedKeys.Contains(b.Key))
                .ToList();

            if (!candidates.Any()) return new List<Badge>();

            var context = await BuildContextAsync();
            var newBadges = new List<Badge>();

            foreach (var badge in candidates)
            {
                if (badge.Condition(context))
                {
                    await _db.SaveEarnedBadgeAsync(new EarnedBadge
                    {
                        BadgeKey = badge.Key,
                        EarnedAt = DateTime.Now
                    });

                    newBadges.Add(new Badge
                    {
                        Definition = badge,
                        IsEarned = true,
                        EarnedAt = DateTime.Now,
                        Progress = 1.0
                    });
                }
            }

            return newBadges;
        }

        private async Task TryAwardBadgeAsync(string key, bool condition)
        {
            if (!condition) return;
            if (await _db.IsBadgeEarnedAsync(key)) return;

            await _db.SaveEarnedBadgeAsync(new EarnedBadge
            {
                BadgeKey = key,
                EarnedAt = DateTime.Now
            });
        }
        public async Task<List<Badge>> GetBadgesAsync()
        {
            var earnedKeys = (await _db.GetEarnedBadgesAsync())
                .ToDictionary(b => b.BadgeKey, b => b.EarnedAt);

            var context = await BuildContextAsync();

            return BadgeDefinitions.All.Select(def => new Badge
            {
                Definition = def,
                IsEarned = earnedKeys.ContainsKey(def.Key),
                EarnedAt = earnedKeys.GetValueOrDefault(def.Key),
                Progress = def.Progress(context)
            }).ToList();
        }

        private async Task<BadgeCheckContext> BuildContextAsync()
        {
            var profile = await _db.GetProfileAsync();
            var todayHabits = await _habitService.GetTodayHabitsAsync();
            var todayLogs = await _db.GetLogsForTodayAsync();
            var completedToday = todayLogs.Select(l => l.HabitId).ToHashSet();
            var completedCount = todayHabits.Count(h => completedToday.Contains(h.Id));

            return new BadgeCheckContext
            {
                TotalLogsCount = await _db.GetTotalLogsCountAsync(),
                CurrentStreak = profile.CurrentStreak,
                Level = profile.Level,
                AllTodayHabitsCompleted = todayHabits.Count > 0 &&
                                          completedCount == todayHabits.Count,
                TodayCompletionRate = todayHabits.Count == 0 ? 0 :
                                      (double)completedCount / todayHabits.Count,
                PerfectDaysStreak = profile.CurrentStreak
            };
        }
    }
}

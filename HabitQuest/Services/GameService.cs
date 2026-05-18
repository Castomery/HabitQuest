using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Data;
using HabitQuest.Interfaces;
using HabitQuest.Models;

namespace HabitQuest.Services
{
    public class GameService : IGameService
    {
        private readonly IDatabaseService _db;

        public GameService(IDatabaseService db)
        {
            _db = db;
        }

        public Task<UserProfile> GetProfileAsync() =>
        _db.GetProfileAsync();

        public async Task<(int xpEarned, bool leveledUp)> CompleteHabitAsync(Habit habit)
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

            await UpdateStreakAsync(profile);

            await _db.SaveProfileAsync(profile);

            await CheckBadgesAsync(profile);

            var leveledUp = profile.Level > levelBefore;
            return (habit.XpReward, leveledUp);
        }

        

        private async Task UpdateStreakAsync(UserProfile profile)
        {
            var today = DateTime.Today;
            var lastDate = profile.LastCompletedDate.Date;

            if (lastDate == today)
                return;

            if (lastDate == today.AddDays(-1))
                profile.CurrentStreak++;
            else
                profile.CurrentStreak = 1;

            if (profile.CurrentStreak > profile.LongestStreak)
                profile.LongestStreak = profile.CurrentStreak;

            profile.LastCompletedDate = today;
        }

        private async Task CheckBadgesAsync(UserProfile profile)
        {
            var logs = await _db.GetLogsForTodayAsync();
            var allLogs = await _db.GetAllLogsAsync();

            await TryAwardBadgeAsync("first_habit", allLogs.Count >= 1);
            await TryAwardBadgeAsync("habits_10", allLogs.Count >= 10);
            await TryAwardBadgeAsync("habits_50", allLogs.Count >= 50);
            await TryAwardBadgeAsync("habits_100", allLogs.Count >= 100);

            await TryAwardBadgeAsync("streak_3", profile.CurrentStreak >= 3);
            await TryAwardBadgeAsync("streak_7", profile.CurrentStreak >= 7);
            await TryAwardBadgeAsync("streak_30", profile.CurrentStreak >= 30);

            await TryAwardBadgeAsync("level_2", profile.Level >= 2);
            await TryAwardBadgeAsync("level_5", profile.Level >= 5);
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
            var earned = await _db.GetEarnedBadgesAsync();
            var earnedKeys = earned.Select(e => e.BadgeKey).ToHashSet();

            return BadgeDefinitions.All.Select(def => new Badge
            {
                Definition = def,
                IsEarned = earnedKeys.Contains(def.Key),
                EarnedAt = earned.FirstOrDefault(e => e.BadgeKey == def.Key)?.EarnedAt
            }).ToList();
        }
    }
}

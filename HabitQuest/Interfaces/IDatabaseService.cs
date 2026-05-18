using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Models;

namespace HabitQuest.Interfaces
{
    public interface IDatabaseService
    {
        Task InitAsync();

        Task<List<Habit>> GetHabitsAsync();
        Task<int> SaveHabitAsync(Habit habit);
        Task<int> DeleteHabitAsync(Habit habit);

        Task<int> SaveLogAsync(HabitLog log);
        Task<List<HabitLog>> GetLogsForTodayAsync();
        Task<List<HabitLog>> GetLogsForHabitAsync(int habitId);
        Task<List<HabitLog>> GetAllLogsAsync();

        Task<UserProfile> GetProfileAsync();
        Task<int> SaveProfileAsync(UserProfile profile);

        Task<List<EarnedBadge>> GetEarnedBadgesAsync();
        Task<int> SaveEarnedBadgeAsync(EarnedBadge badge);
        Task<bool> IsBadgeEarnedAsync(string key);
    }
}

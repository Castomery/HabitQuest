using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Interfaces;
using HabitQuest.Models;
using SQLite;

namespace HabitQuest.Services
{
    public class DatabaseService : IDatabaseService
    {
        private SQLiteAsyncConnection _db = null!;

        public async Task InitAsync()
        {
            if (_db is not null) return;

            var dbPath = Path.Combine(
                FileSystem.AppDataDirectory, "habitquest.db3");

            _db = new SQLiteAsyncConnection(dbPath);

            await _db.CreateTableAsync<Habit>();
            await _db.CreateTableAsync<HabitLog>();
            await _db.CreateTableAsync<UserProfile>();
            await _db.CreateTableAsync<EarnedBadge>();

            var profile = await _db.FindAsync<UserProfile>(1);
            if (profile is null)
                await _db.InsertAsync(new UserProfile());
        }

        public Task<List<Habit>> GetHabitsAsync() =>
       _db.Table<Habit>().Where(h => !h.IsArchived).ToListAsync();

        public Task<int> SaveHabitAsync(Habit habit) =>
            habit.Id == 0 ? _db.InsertAsync(habit) : _db.UpdateAsync(habit);

        public Task<int> DeleteHabitAsync(Habit habit) =>
            _db.DeleteAsync(habit);


        public Task<int> SaveLogAsync(HabitLog log) =>
        _db.InsertAsync(log);

        public Task<List<HabitLog>> GetLogsForTodayAsync() =>
            _db.Table<HabitLog>()
               .Where(l => l.CompletedAt.Date == DateTime.Today)
               .ToListAsync();

        public Task<List<HabitLog>> GetLogsForHabitAsync(int habitId) =>
            _db.Table<HabitLog>()
               .Where(l => l.HabitId == habitId)
               .ToListAsync();
        public Task<List<HabitLog>> GetAllLogsAsync() =>
        _db.Table<HabitLog>().ToListAsync();
        public Task<int> GetTotalLogsCountAsync() =>
        _db.Table<HabitLog>().CountAsync();


        public Task<UserProfile> GetProfileAsync() =>
        _db.FindAsync<UserProfile>(1);

        public Task<int> SaveProfileAsync(UserProfile profile) =>
            _db.UpdateAsync(profile);


        public Task<List<EarnedBadge>> GetEarnedBadgesAsync() =>
        _db.Table<EarnedBadge>().ToListAsync();

        public Task<int> SaveEarnedBadgeAsync(EarnedBadge badge) =>
            _db.InsertAsync(badge);

        public async Task<bool> IsBadgeEarnedAsync(string key) =>
            await _db.Table<EarnedBadge>()
                     .Where(b => b.BadgeKey == key)
                     .CountAsync() > 0;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Interfaces;
using HabitQuest.Models;

namespace HabitQuest.Services
{
    public class HabitService : IHabitService
    {

        private readonly IDatabaseService _db;

        public HabitService(IDatabaseService db)
        {
            _db = db;
        }

        public async Task<List<Habit>> GetTodayHabitsAsync()
        {
            var allHabits = await _db.GetHabitsAsync();
            var today = DateTime.Today.DayOfWeek;

            return allHabits
                .Where(h => h.Days.Contains(today))
                .ToList();
        }

        public Task<List<Habit>> GetAllHabitsAsync() =>
            _db.GetHabitsAsync();

        public Task SaveHabitAsync(Habit habit) =>
            _db.SaveHabitAsync(habit);

        public Task DeleteHabitAsync(Habit habit) =>
            _db.DeleteHabitAsync(habit);

        public async Task<bool> IsCompletedTodayAsync(int habitId)
        {
            var logs = await _db.GetLogsForTodayAsync();
            return logs.Any(l => l.HabitId == habitId);
        }

        public Task<List<HabitLog>> GetLogsForHabitAsync(int habitId) =>
            _db.GetLogsForHabitAsync(habitId);
    }
}

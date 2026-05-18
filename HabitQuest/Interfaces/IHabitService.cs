using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Models;

namespace HabitQuest.Interfaces
{
    public interface IHabitService
    {
        Task<List<Habit>> GetTodayHabitsAsync();
        Task<List<Habit>> GetAllHabitsAsync();
        Task SaveHabitAsync(Habit habit);
        Task DeleteHabitAsync(Habit habit);
        Task<bool> IsCompletedTodayAsync(int habitId);
        Task<List<HabitLog>> GetLogsForHabitAsync(int habitId);
    }
}

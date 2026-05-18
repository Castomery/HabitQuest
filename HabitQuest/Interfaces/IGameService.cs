using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Models;

namespace HabitQuest.Interfaces
{
    public interface IGameService
    {
        Task<UserProfile> GetProfileAsync();
        Task<(int xpEarned, bool leveledUp)> CompleteHabitAsync(Habit habit);
        Task<List<Badge>> GetBadgesAsync();
    }
}

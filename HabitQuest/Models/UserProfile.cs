using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HabitQuest.Models
{
    public class UserProfile
    {
        [PrimaryKey]
        public int Id { get; set; } = 1;
        public int TotalXp { get; set; }
        public int CurrentStreak { get; set; }
        public int LongestStreak { get; set; }
        public DateTime LastCompletedDate { get; set; }

        [Ignore]
        public int Level => TotalXp switch
        {
            < 100 => 1,
            < 300 => 2,
            < 600 => 3,
            < 1000 => 4,
            _ => 5
        };

        [Ignore]
        public int XpForNextLevel => Level switch
        {
            1 => 100,
            2 => 300,
            3 => 600,
            4 => 1000,
            _ => 1000
        };

        [Ignore]
        public double LevelProgress =>
            (double)TotalXp / XpForNextLevel;
    }
}

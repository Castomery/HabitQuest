using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitQuest.Models
{
    public class BadgeCheckContext
    {
        public int TotalLogsCount { get; set; }
        public int CurrentStreak { get; set; }
        public int Level { get; set; }
        public bool AllTodayHabitsCompleted { get; set; }
        public double TodayCompletionRate { get; set; }
        public int PerfectDaysStreak { get; set; }
    }
}

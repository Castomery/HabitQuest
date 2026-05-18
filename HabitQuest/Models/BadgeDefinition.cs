using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Enums;
using SQLite;

namespace HabitQuest.Models
{
    public class BadgeDefinition
    {
        public string Key { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;

        public BadgeTrigger Trigger { get; set; }
        public Func<BadgeCheckContext, bool> Condition { get; set; } = _ => false;
        public Func<BadgeCheckContext, double> Progress { get; set; } = _ => 0;

    }
}

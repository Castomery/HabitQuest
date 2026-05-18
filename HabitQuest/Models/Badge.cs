using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitQuest.Models
{
    public class Badge
    {
        public BadgeDefinition Definition { get; set; } = null!;
        public bool IsEarned { get; set; }
        public DateTime? EarnedAt { get; set; }

        public string Icon => Definition.Icon;
        public string Name => Definition.Name;
        public string Description => Definition.Description;
    }
}

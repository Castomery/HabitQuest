using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HabitQuest.Models
{
    public class EarnedBadge
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string BadgeKey { get; set; } = string.Empty;
        public DateTime EarnedAt { get; set; } = DateTime.Now;
    }
}

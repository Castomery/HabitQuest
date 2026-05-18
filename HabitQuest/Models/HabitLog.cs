using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HabitQuest.Models
{
    public class HabitLog
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int HabitId { get; set; }
        public DateTime CompletedAt { get; set; } = DateTime.Now;
        public int XpEarned { get; set; }
    }
}

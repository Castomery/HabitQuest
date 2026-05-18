using HabitQuest.Enums;
using SQLite;

namespace HabitQuest.Models
{
    public class Habit
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string DaysOfWeek { get; set; } = string.Empty;
        public Difficulty Difficulty { get; set; } = Difficulty.Medium;
        public bool IsFromTemplate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public bool IsArchived { get; set; }

        [Ignore]
        public int XpReward => Difficulty switch
        {
            Difficulty.Easy => 5,
            Difficulty.Medium => 10,
            Difficulty.Hard => 20,
            _ => 10
        };

        [Ignore]
        public List<DayOfWeek> Days =>
            DaysOfWeek.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(d => Enum.Parse<DayOfWeek>(d))
                      .ToList();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using HabitQuest.Enums;
using HabitQuest.Models;

namespace HabitQuest.ViewModels
{
    public partial class HabitItemViewModel : ObservableObject
    {
        public Habit Habit { get; }

        [ObservableProperty]
        private bool _isCompleted;

        public HabitItemViewModel(Habit habit, bool isCompleted)
        {
            Habit = habit;
            IsCompleted = isCompleted;
        }

        public string Name => Habit.Name;
        public string Category => Habit.Category;
        public string DifficultyLabel => Habit.Difficulty switch
        {
            Difficulty.Easy => "Легка • +5 XP",
            Difficulty.Medium => "Середня • +10 XP",
            Difficulty.Hard => "Важка • +20 XP",
            _ => string.Empty
        };

        public string CategoryIcon => Habit.Category switch
        {
            "Здоров'я" => "❤️",
            "Спорт" => "🏃",
            "Навчання" => "📚",
            "Продуктивність" => "⚡",
            _ => "✨"
        };

        
    }
}

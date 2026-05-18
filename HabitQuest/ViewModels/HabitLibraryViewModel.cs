using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitQuest.Data;
using HabitQuest.Models;

namespace HabitQuest.ViewModels
{
    public partial class HabitLibraryViewModel : ObservableObject
    {
        [ObservableProperty]
        private List<string> _categories = new();

        [ObservableProperty]
        private string _selectedCategory = string.Empty;

        [ObservableProperty]
        private List<Habit> _templates = new();

        public Habit? SelectedTemplate { get; private set; }

        public HabitLibraryViewModel()
        {
            Categories = HabitTemplates.GetCategories();
            SelectedCategory = Categories.FirstOrDefault() ?? string.Empty;
            LoadTemplates();
        }

        partial void OnSelectedCategoryChanged(string value)
        {
            LoadTemplates();
        }

        private void LoadTemplates()
        {
            Templates = HabitTemplates.GetByCategory(SelectedCategory);
        }

        [RelayCommand]
        public void SelectTemplate(Habit template)
        {
            SelectedTemplate = new Habit
            {
                Name = template.Name,
                Category = template.Category,
                Difficulty = template.Difficulty,
                DaysOfWeek = template.DaysOfWeek,
                IsFromTemplate = true
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HabitQuest.Data;
using HabitQuest.Interfaces;
using HabitQuest.Messages;
using HabitQuest.Models;

namespace HabitQuest.ViewModels
{
    public partial class HabitLibraryViewModel : ObservableObject
    {
        private readonly IHabitService _habitService;

        [ObservableProperty]
        private List<string> _filterCategories = new();

        [ObservableProperty]
        private string _selectedCategory = "Всі";

        [ObservableProperty]
        private List<HabitItemViewModel> _templates = new();

        public event EventHandler? CloseRequested;

        public HabitLibraryViewModel(IHabitService habitService)
        {
            _habitService = habitService;

            FilterCategories = new List<string> { "Всі" }
                .Concat(HabitTemplates.GetCategories())
                .ToList();

            LoadTemplates();
        }

        partial void OnSelectedCategoryChanged(string value) => LoadTemplates();

        private void LoadTemplates()
        {
            var habits = SelectedCategory == "Всі"
                ? HabitTemplates.All
                : HabitTemplates.GetByCategory(SelectedCategory);

            Templates = habits.Select(h => new HabitItemViewModel(h, false)).ToList();
        }

        [RelayCommand]
        public void SelectCategory(string category)
        {
            SelectedCategory = category;
        }

        [RelayCommand]
        public async Task SelectTemplateAsync(HabitItemViewModel item)
        {
            var habit = new Habit
            {
                Name = item.Habit.Name,
                Category = item.Habit.Category,
                Difficulty = item.Habit.Difficulty,
                DaysOfWeek = item.Habit.DaysOfWeek,
                IsFromTemplate = true
            };

            await _habitService.SaveHabitAsync(habit);
            WeakReferenceMessenger.Default.Send(new HabitSavedMessage());
            await Toast.Make($"«{habit.Name}» додано ✅").Show();
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        public void Close()
        {
            CloseRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}

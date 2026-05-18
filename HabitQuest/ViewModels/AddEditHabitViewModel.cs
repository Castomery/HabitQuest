using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Xml.Linq;
using HabitQuest.Interfaces;
using HabitQuest.Models;
using HabitQuest.Enums;
using HabitQuest.Messages;

namespace HabitQuest.ViewModels
{
    public partial class AddEditHabitViewModel : ObservableObject
    {
        private readonly IHabitService _habitService;
        private readonly Habit? _existingHabit;

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private string _category = "Здоров'я";

        [ObservableProperty]
        private Difficulty _difficulty = Difficulty.Medium;

        [ObservableProperty]
        private List<DayOfWeek> _selectedDays = new();

        public bool IsEditing => _existingHabit is not null;
        public string Title => IsEditing ? "Редагувати звичку" : "Нова звичка";
        public string SaveButtonText => IsEditing ? "Зберегти" : "Додати";

        public List<string> Categories => new()
    {
        "Здоров'я", "Спорт", "Навчання", "Продуктивність", "Інше"
    };

        public List<DayOfWeek> AllDays => new()
    {
        DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday,
        DayOfWeek.Thursday, DayOfWeek.Friday, DayOfWeek.Saturday,
        DayOfWeek.Sunday
    };

        public AddEditHabitViewModel(IHabitService habitService, Habit? existingHabit = null)
        {
            _habitService = habitService;
            _existingHabit = existingHabit;

            if (existingHabit is not null)
                PopulateFromHabit(existingHabit);
        }

        private void PopulateFromHabit(Habit habit)
        {
            Name = habit.Name;
            Category = habit.Category;
            Difficulty = habit.Difficulty;
            SelectedDays = habit.Days;
        }

        [RelayCommand]
        public void ToggleDay(DayOfWeek day)
        {
            if (SelectedDays.Contains(day))
                SelectedDays = SelectedDays.Where(d => d != day).ToList();
            else
                SelectedDays = SelectedDays.Append(day).ToList();
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                await Toast.Make("Введи назву звички").Show();
                return;
            }

            if (!SelectedDays.Any())
            {
                await Toast.Make("Вибери хоча б один день").Show();
                return;
            }

            var habit = _existingHabit ?? new Habit();
            habit.Name = Name.Trim();
            habit.Category = Category;
            habit.Difficulty = Difficulty;
            habit.DaysOfWeek = string.Join(",", SelectedDays);

            await _habitService.SaveHabitAsync(habit);

            WeakReferenceMessenger.Default.Send(new HabitSavedMessage());

            await Toast.Make(IsEditing ? "Звичку оновлено ✅" : "Звичку додано ✅").Show();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitQuest.Interfaces;
using HabitQuest.Models;

namespace HabitQuest.ViewModels
{
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IHabitService _habitService;
        private readonly IGameService _gameService;

        [ObservableProperty]
        private ObservableCollection<HabitItemViewModel> _todayHabits = new();

        [ObservableProperty]
        private UserProfile _profile = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string _greetingMessage = string.Empty;

        public bool IsEmpty => _todayHabits.Any();

        public DashboardViewModel(IHabitService habitService, IGameService gameService)
        {
            _habitService = habitService;
            _gameService = gameService;
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            IsLoading = true;

            Profile = await _gameService.GetProfileAsync();

            var habits = await _habitService.GetTodayHabitsAsync();
            var items = new ObservableCollection<HabitItemViewModel>();

            foreach (var habit in habits)
            {
                var isCompleted = await _habitService.IsCompletedTodayAsync(habit.Id);
                items.Add(new HabitItemViewModel(habit, isCompleted));
            }

            TodayHabits = items;
            GreetingMessage = GetGreeting();
            IsLoading = false;
        }

        [RelayCommand]
        public async Task CompleteHabitAsync(HabitItemViewModel item)
        {
            if (item.IsCompleted) return;

            var (xpEarned, leveledUp, newBadges) =
                await _gameService.CompleteHabitAsync(item.Habit);

            item.IsCompleted = true;
            Profile = await _gameService.GetProfileAsync();

            await Toast.Make($"+{xpEarned} XP 💪").Show();

            foreach (var badge in newBadges)
            {
                await Task.Delay(1000);
                await Toast.Make($"Новий бейдж: {badge.Icon} {badge.Name}!").Show();
            }

            if (leveledUp)
            {
                await Task.Delay(500);
                await Shell.Current.DisplayAlert(
                    "Новий рівень! 🎉",
                    $"Вітаємо! Ти досяг {Profile.Level} рівня!",
                    "Круто!");
            }
        }

        public string LevelTitle => Profile.Level switch
        {
            1 => "Новачок 🌱",
            2 => "Учень 📖",
            3 => "Практик 💪",
            4 => "Майстер ⚔️",
            5 => "Легенда 👑",
            _ => string.Empty
        };

        partial void OnProfileChanged(UserProfile value)
        {
            OnPropertyChanged(nameof(LevelTitle));
        }

        private string GetGreeting()
        {
            var hour = DateTime.Now.Hour;
            return hour switch
            {
                < 12 => "Доброго ранку! ☀️",
                < 17 => "Гарного дня! 👋",
                _ => "Гарного вечора! 🌙"
            };
        }
    }
}

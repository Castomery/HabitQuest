using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitQuest.Interfaces;
using HabitQuest.Models;
using Plugin.LocalNotification;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HabitQuest.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly IGameService _gameService;
        private readonly IDatabaseService _databaseService;
        private readonly IHabitNotificationService _notificationService;


        [ObservableProperty]
        private UserProfile _profile = new();

        [ObservableProperty]
        private List<Badge> _badges = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private int _totalCompletions;

        [ObservableProperty]
        private bool _isNotificationEnabled;

        [ObservableProperty]
        private TimeSpan _notificationTime = new TimeSpan(9, 0, 0);

        public string AvatarEmoji => Profile.Level switch
        {
            1 => "🌱",
            2 => "📖",
            3 => "💪",
            4 => "⚔️",
            5 => "👑",
            _ => "🌱"
        };

        public ProfileViewModel(IGameService gameService, IDatabaseService databaseService, IHabitNotificationService notificationService)
        {
            _gameService = gameService;
            _databaseService = databaseService;

            _notificationService = notificationService;

            var savedTime = _notificationService.GetScheduledTime();
            IsNotificationEnabled = savedTime.HasValue;
            if (savedTime.HasValue)
                NotificationTime = savedTime.Value;
        }

        public async Task ToggleNotificationAsync()
        {
            if (IsNotificationEnabled)
            {
                await _notificationService.RequestPermissionAsync();
                await _notificationService.ScheduleDailyReminderAsync(NotificationTime);
                await Toast.Make($"Нагадування встановлено на {NotificationTime:hh\\:mm} ✅").Show();
            }
            else
            {
                await _notificationService.CancelAllAsync();
                await Toast.Make("Нагадування вимкнено").Show();
            }
        }

        partial void OnNotificationTimeChanged(TimeSpan value)
        {
            if (IsNotificationEnabled)
                _ = _notificationService.ScheduleDailyReminderAsync(value);
        }

        partial void OnIsNotificationEnabledChanged(bool value)
        {
            _ = ToggleNotificationAsync();
        }

        partial void OnProfileChanged(UserProfile value)
        {
            OnPropertyChanged(nameof(LevelTitle));
            OnPropertyChanged(nameof(AvatarEmoji));
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            IsLoading = true;
            Profile = await _gameService.GetProfileAsync();
            Badges = await _gameService.GetBadgesAsync();
            TotalCompletions = await _databaseService.GetTotalLogsCountAsync();
            IsLoading = false;
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
    }
}

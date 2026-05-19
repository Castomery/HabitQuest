using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitQuest.Interfaces;
using HabitQuest.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HabitQuest.ViewModels
{
    public partial class ProfileViewModel : ObservableObject
    {
        private readonly IGameService _gameService;
        private readonly IDatabaseService _databaseService;

        [ObservableProperty]
        private UserProfile _profile = new();

        [ObservableProperty]
        private List<Badge> _badges = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private int _totalCompletions;

        public string AvatarEmoji => Profile.Level switch
        {
            1 => "🌱",
            2 => "📖",
            3 => "💪",
            4 => "⚔️",
            5 => "👑",
            _ => "🌱"
        };

        public ProfileViewModel(IGameService gameService, IDatabaseService databaseService)
        {
            _gameService = gameService;
            _databaseService = databaseService;
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

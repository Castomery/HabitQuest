using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using HabitQuest.Interfaces;
using HabitQuest.Messages;
using HabitQuest.Models;
using HabitQuest.Views;

namespace HabitQuest.ViewModels
{
    public partial class HabitsViewModel : ObservableObject, IRecipient<HabitSavedMessage>
    {
        private readonly IHabitService _habitService;

        [ObservableProperty]
        private ObservableCollection<HabitItemViewModel> _habits = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _isEmpty;

        public event EventHandler<bool>? OpenAddHabitRequested;

        public HabitsViewModel(IHabitService habitService)
        {
            _habitService = habitService;
            WeakReferenceMessenger.Default.Register(this);
        }

        [RelayCommand]
        public async Task LoadAsync()
        {
            IsLoading = true;

            var habits = await _habitService.GetAllHabitsAsync();

            Habits = new ObservableCollection<HabitItemViewModel>(
                habits.Select(h => new HabitItemViewModel(h, false))
            );

            IsEmpty = !Habits.Any();
            IsLoading = false;
        }

        [RelayCommand]
        public async Task DeleteHabitAsync(HabitItemViewModel item)
        {
            var confirm = await Shell.Current.DisplayAlert(
                "Видалити звичку?",
                $"Ти впевнений що хочеш видалити \"{item.Name}\"?",
                "Видалити",
                "Скасувати");

            if (!confirm) return;

            await _habitService.DeleteHabitAsync(item.Habit);
            Habits.Remove(item);
            IsEmpty = !Habits.Any();
        }

        [RelayCommand]
        public async Task AddHabitAsync()
        {
            var result = await Shell.Current.DisplayActionSheet(
                "Як додати звичку?",
                "Скасувати",
                null,
                "📚 З бібліотеки",
                "✏️ Власна звичка");

            if (result == "📚 З бібліотеки")
            {
                // TODO: відкрити бібліотеку
            }
            else if (result == "✏️ Власна звичка")
            {
                OpenAddHabitRequested?.Invoke(this, false);
            }
        }

        //[RelayCommand]
        //public async Task HabitSavedAsync(Habit habit)
        //{
        //    await LoadAsync();
        //}

        public async void Receive(HabitSavedMessage message)
        {
            await LoadAsync();
        }
    }
}

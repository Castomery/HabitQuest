using CommunityToolkit.Maui.Views;
using HabitQuest.Interfaces;
using HabitQuest.ViewModels;

namespace HabitQuest.Views;

public partial class HabitsPage : ContentPage
{
    private readonly HabitsViewModel _viewModel;
    private readonly IServiceProvider _serviceProvider;

    public HabitsPage(HabitsViewModel viewModel, IServiceProvider serviceProvider)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _serviceProvider = serviceProvider;
        BindingContext = viewModel;
        _viewModel.OpenAddHabitRequested += OnOpenAddHabitRequested;
        _viewModel.EditHabitRequested += OnEditHabitRequested;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
    private async void OnOpenAddHabitRequested(object? sender, bool fromLibrary)
    {
        if (fromLibrary)
        {
            var viewModel = _serviceProvider.GetRequiredService<HabitLibraryViewModel>();
            var page = new HabitLibraryPage(viewModel);
            await Navigation.PushModalAsync(page);
        }
        else
        {
            var viewModel = _serviceProvider.GetRequiredService<AddEditHabitViewModel>();
            var page = new AddEditHabitBottomSheet(viewModel);
            await this.ShowPopupAsync(page);
        }
    }

    private async void OnEditHabitRequested(object? sender, HabitItemViewModel item)
    {
        var viewModel = new AddEditHabitViewModel(_serviceProvider.GetRequiredService<IHabitService>(), item.Habit);
        var page = new AddEditHabitBottomSheet(viewModel);
        await this.ShowPopupAsync(page);
    }
}
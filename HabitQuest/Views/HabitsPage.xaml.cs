using HabitQuest.ViewModels;

namespace HabitQuest.Views;

public partial class HabitsPage : ContentPage
{
    private readonly HabitsViewModel _viewModel;

    public HabitsPage(HabitsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}
using CommunityToolkit.Maui.Views;
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
    }

    private async void OnOpenAddHabitRequested(object? sender, bool fromLibrary)
    {
        var viewModel = _serviceProvider.GetRequiredService<AddEditHabitViewModel>();
        var popup = new AddEditHabitBottomSheet(viewModel);
        await this.ShowPopupAsync(popup);
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}
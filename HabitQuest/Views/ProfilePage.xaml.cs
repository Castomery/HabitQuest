using HabitQuest.ViewModels;

namespace HabitQuest.Views;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel _viewModel;
    public ProfilePage(ProfileViewModel profileViewModel)
	{
		InitializeComponent();
        _viewModel = profileViewModel;
        BindingContext = profileViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadAsync();
    }
}
using HabitQuest.ViewModels;

namespace HabitQuest.Views;

public partial class HabitLibraryPage : ContentPage
{
	public HabitLibraryPage(HabitLibraryViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;

        viewModel.CloseRequested += async (s, e) =>
            await Navigation.PopModalAsync();
    }
}
using CommunityToolkit.Maui.Views;
using HabitQuest.ViewModels;

namespace HabitQuest.Views;


public partial class AddEditHabitBottomSheet : Popup
{
    public AddEditHabitBottomSheet(AddEditHabitViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

        viewModel.CloseRequested += (s, e) => Close();
    }
}
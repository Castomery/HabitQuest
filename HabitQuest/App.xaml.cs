
using HabitQuest.Interfaces;

namespace HabitQuest
{
    public partial class App : Application
    {
        public App(IDatabaseService databaseService)
        {
            InitializeComponent();
            MainPage = new ContentPage();

            Dispatcher.Dispatch(async () =>
            {
                await databaseService.InitAsync();
                MainPage = new AppShell();
            });
        }

        private static async Task InitializeDatabaseAsync(IDatabaseService databaseService)
        {
            await databaseService.InitAsync();
        }
    }
}
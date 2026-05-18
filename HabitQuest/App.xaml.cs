
using HabitQuest.Interfaces;

namespace HabitQuest
{
    public partial class App : Application
    {
        public App(IDatabaseService databaseService)
        {
            InitializeComponent();
            InitializeDatabaseAsync(databaseService).ConfigureAwait(false);
            MainPage = new AppShell();
        }

        private static async Task InitializeDatabaseAsync(IDatabaseService databaseService)
        {
            await databaseService.InitAsync();
        }
    }
}
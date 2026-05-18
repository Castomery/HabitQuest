using CommunityToolkit.Maui;
using HabitQuest.Interfaces;
using HabitQuest.Services;
using HabitQuest.ViewModels;
using HabitQuest.Views;
using Microsoft.Extensions.Logging;

namespace HabitQuest
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
            builder.Services.AddSingleton<IHabitService, HabitService>();
            builder.Services.AddSingleton<IGameService, GameService>();

            builder.Services.AddTransient<DashboardViewModel>();
            builder.Services.AddTransient<HabitsViewModel>();
            builder.Services.AddTransient<AddEditHabitViewModel>();
            builder.Services.AddTransient<HabitLibraryViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();

            builder.Services.AddTransient<DashboardPage>();
            builder.Services.AddTransient<HabitsPage>();
            builder.Services.AddTransient<ProfilePage>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Interfaces;
using Plugin.LocalNotification.AndroidOption;
using Plugin.LocalNotification;


namespace HabitQuest.Services
{
    public class NotificationService : IHabitNotificationService
    {
        private const string TimeKey = "notification_time";
        private const int NotificationId = 1001;

        public async Task RequestPermissionAsync()
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();
        }

        public async Task ScheduleDailyReminderAsync(TimeSpan time)
        {
            await CancelAllAsync();

            var now = DateTime.Now;
            var scheduledTime = DateTime.Today.Add(time);

            if (scheduledTime <= now)
                scheduledTime = scheduledTime.AddDays(1);

            var request = new NotificationRequest
            {
                NotificationId = NotificationId,
                Title = "Час для звичок! 💪",
                Description = "Не забудь виконати свої щоденні звички",
                ReturningData = "reminder",
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = scheduledTime,
                    RepeatType = NotificationRepeat.Daily
                },
                Android = new AndroidOptions
                {
                    ChannelId = "habit_reminder",
                    Priority = AndroidPriority.High
                }
            };

            await LocalNotificationCenter.Current.Show(request);

            Preferences.Set(TimeKey, time.TotalMinutes);
        }

        public async Task CancelAllAsync()
        {
            await Task.Run(() =>
                LocalNotificationCenter.Current.Cancel(NotificationId));
        }

        public TimeSpan? GetScheduledTime()
        {
            var minutes = Preferences.Get(TimeKey, -1.0);
            return minutes < 0 ? null : TimeSpan.FromMinutes(minutes);
        }
    }
}

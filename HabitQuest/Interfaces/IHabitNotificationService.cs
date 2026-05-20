using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitQuest.Interfaces
{
    public interface IHabitNotificationService
    {
        Task RequestPermissionAsync();
        Task ScheduleDailyReminderAsync(TimeSpan time);
        Task CancelAllAsync();
        TimeSpan? GetScheduledTime();
    }
}

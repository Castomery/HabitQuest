using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitQuest.Converters
{
    public class DayToColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is List<DayOfWeek> days && parameter is string param &&
                Enum.TryParse<DayOfWeek>(param, out var day))
                return days.Contains(day) ? Color.FromArgb("#6C63FF") : Color.FromArgb("#E8E8E8");

            return Color.FromArgb("#E0E0E0");
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

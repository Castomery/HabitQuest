using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

namespace HabitQuest.Converters
{
    public class DayToTextColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is List<DayOfWeek> days && parameter is string param &&
                Enum.TryParse<DayOfWeek>(param, out var day))
                return days.Contains(day) ? Colors.White : Color.FromArgb("#1A1A2E");

            return Color.FromArgb("#1A1A2E");
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

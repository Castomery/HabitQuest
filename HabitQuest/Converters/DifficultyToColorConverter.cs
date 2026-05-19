using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Enums;

namespace HabitQuest.Converters
{
    public class DifficultyToColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Difficulty current && parameter is string param &&
                Enum.TryParse<Difficulty>(param, out var target))
                return current == target ? Color.FromArgb("#6C63FF") : Color.FromArgb("#E0E0E0");

            return Color.FromArgb("#E0E0E0");
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

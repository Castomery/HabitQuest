using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Enums;

namespace HabitQuest.Converters
{
    public class DifficultyToTextColorConverter : IValueConverter
    {
        public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Difficulty current && parameter is string param &&
                Enum.TryParse<Difficulty>(param, out var target))
                return current == target ? Colors.White : Color.FromArgb("#1A1A2E");

            return Color.FromArgb("#1A1A2E");
        }

        public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
            => throw new NotImplementedException();
    }
}

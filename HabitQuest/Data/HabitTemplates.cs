using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Enums;
using HabitQuest.Models;

namespace HabitQuest.Data
{
    public static class HabitTemplates
    {
        public static readonly List<Habit> All = new()
    {
        // Здоров'я
        new() { Name = "Пити 2л води", Category = "Здоров'я",
                Difficulty = Difficulty.Easy, DaysOfWeek = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday" },
        new() { Name = "Вітаміни", Category = "Здоров'я",
                Difficulty = Difficulty.Easy, DaysOfWeek = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday" },
        new() { Name = "Лягати до 23:00", Category = "Здоров'я",
                Difficulty = Difficulty.Medium, DaysOfWeek = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday" },

        // Спорт
        new() { Name = "Зарядка 10 хв", Category = "Спорт",
                Difficulty = Difficulty.Easy, DaysOfWeek = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday" },
        new() { Name = "Пробіжка", Category = "Спорт",
                Difficulty = Difficulty.Hard, DaysOfWeek = "Monday,Wednesday,Friday" },
        new() { Name = "Розтяжка", Category = "Спорт",
                Difficulty = Difficulty.Easy, DaysOfWeek = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday" },

        // Навчання
        new() { Name = "Читати 20 хв", Category = "Навчання",
                Difficulty = Difficulty.Easy, DaysOfWeek = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday" },
        new() { Name = "Англійське слово", Category = "Навчання",
                Difficulty = Difficulty.Easy, DaysOfWeek = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday" },
        new() { Name = "Програмування 1 год", Category = "Навчання",
                Difficulty = Difficulty.Hard, DaysOfWeek = "Monday,Tuesday,Wednesday,Thursday,Friday" },

        // Продуктивність
        new() { Name = "Планування дня", Category = "Продуктивність",
                Difficulty = Difficulty.Easy, DaysOfWeek = "Monday,Tuesday,Wednesday,Thursday,Friday" },
        new() { Name = "Без телефону 1 год", Category = "Продуктивність",
                Difficulty = Difficulty.Medium, DaysOfWeek = "Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday" },
        new() { Name = "Прибирання 15 хв", Category = "Продуктивність",
                Difficulty = Difficulty.Easy, DaysOfWeek = "Monday,Wednesday,Friday" },
    };

        public static List<Habit> GetByCategory(string category) =>
            All.Where(h => h.Category == category).ToList();

        public static List<string> GetCategories() =>
            All.Select(h => h.Category).Distinct().ToList();
    }
}

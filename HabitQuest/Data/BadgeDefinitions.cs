using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Models;

namespace HabitQuest.Data
{
    public static class BadgeDefinitions
    {
        public static readonly List<BadgeDefinition> All = new()
    {
        // Перші кроки
        new() {
            Key = "first_habit",
            Name = "Перший крок",
            Description = "Виконай свою першу звичку",
            Icon = "⚡"
        },
        new() {
            Key = "habits_10",
            Name = "Набираю оберти",
            Description = "Виконай звички 10 разів загалом",
            Icon = "💪"
        },
        new() {
            Key = "habits_50",
            Name = "Машина звичок",
            Description = "Виконай звички 50 разів загалом",
            Icon = "🤖"
        },
        new() {
            Key = "habits_100",
            Name = "Легенда",
            Description = "Виконай звички 100 разів загалом",
            Icon = "👑"
        },

        // Streak
        new() {
            Key = "streak_3",
            Name = "Три дні поспіль",
            Description = "Підтримуй streak 3 дні",
            Icon = "🔥"
        },
        new() {
            Key = "streak_7",
            Name = "Тиждень сили",
            Description = "Підтримуй streak 7 днів",
            Icon = "🔥"
        },
        new() {
            Key = "streak_30",
            Name = "Місяць без зупинки",
            Description = "Підтримуй streak 30 днів",
            Icon = "🏆"
        },

        // Рівні
        new() {
            Key = "level_2",
            Name = "Зростання",
            Description = "Досягни 2 рівня",
            Icon = "⬆️"
        },
        new() {
            Key = "level_5",
            Name = "Майстер звичок",
            Description = "Досягни 5 рівня",
            Icon = "🌟"
        },

        // Perfect day
        new() {
            Key = "perfect_day",
            Name = "Ідеальний день",
            Description = "Виконай всі звички за один день",
            Icon = "✨"
        },
        new() {
            Key = "perfect_week",
            Name = "Ідеальний тиждень",
            Description = "Виконай всі звички 7 днів поспіль",
            Icon = "🎯"
        },
    };

        public static BadgeDefinition? GetByKey(string key) =>
            All.FirstOrDefault(b => b.Key == key);
    }
}

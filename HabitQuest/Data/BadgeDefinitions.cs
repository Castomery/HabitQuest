using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitQuest.Enums;
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
            Icon = "⚡",
            Trigger = BadgeTrigger.OnHabitCompleted,
            Condition = ctx => ctx.TotalLogsCount >= 1,
            Progress = ctx => Math.Min((double)ctx.TotalLogsCount / 1, 1.0)
        },
        new() {
            Key = "habits_10",
            Name = "Набираю оберти",
            Description = "Виконай звички 10 разів загалом",
            Icon = "💪",
            Trigger = BadgeTrigger.OnHabitCompleted,
            Condition = ctx => ctx.TotalLogsCount >= 10,
            Progress = ctx => Math.Min((double)ctx.TotalLogsCount / 10, 1.0)
        },
        new() {
            Key = "habits_50",
            Name = "Машина звичок",
            Description = "Виконай звички 50 разів загалом",
            Icon = "🤖",
            Trigger = BadgeTrigger.OnHabitCompleted,
            Condition = ctx => ctx.TotalLogsCount >= 50,
            Progress = ctx => Math.Min((double)ctx.TotalLogsCount / 50, 1.0)
        },
        new() {
            Key = "habits_100",
            Name = "Легенда",
            Description = "Виконай звички 100 разів загалом",
            Icon = "👑",
            Trigger = BadgeTrigger.OnHabitCompleted,
            Condition = ctx => ctx.TotalLogsCount >= 100,
            Progress = ctx => Math.Min((double)ctx.TotalLogsCount / 100, 1.0)
        },

        // Streak
        new() {
            Key = "streak_3",
            Name = "Три дні поспіль",
            Description = "Підтримуй streak 3 дні",
            Icon = "🔥",
            Trigger = BadgeTrigger.OnStreakUpdated,
            Condition = ctx => ctx.CurrentStreak >= 3,
            Progress = ctx => Math.Min((double)ctx.CurrentStreak / 3, 1.0)
        },
        new() {
            Key = "streak_7",
            Name = "Тиждень сили",
            Description = "Підтримуй streak 7 днів",
            Icon = "🔥",
            Trigger = BadgeTrigger.OnStreakUpdated,
            Condition = ctx => ctx.CurrentStreak >= 7,
            Progress = ctx => Math.Min((double)ctx.CurrentStreak / 7, 1.0)
        },
        new() {
            Key = "streak_30",
            Name = "Місяць без зупинки",
            Description = "Підтримуй streak 30 днів",
            Icon = "🏆",
            Trigger = BadgeTrigger.OnStreakUpdated,
            Condition = ctx => ctx.CurrentStreak >= 30,
            Progress = ctx => Math.Min((double)ctx.CurrentStreak / 30, 1.0)
        },

        // Рівні
        new() {
            Key = "level_2",
            Name = "Зростання",
            Description = "Досягни 2 рівня",
            Icon = "⬆️",
            Trigger = BadgeTrigger.OnLevelUp,
            Condition = ctx => ctx.Level >= 2,
            Progress = ctx => Math.Min((double)ctx.Level / 2, 1.0)
        },
        new() {
            Key = "level_5",
            Name = "Майстер звичок",
            Description = "Досягни 5 рівня",
            Icon = "🌟",
            Trigger = BadgeTrigger.OnLevelUp,
            Condition = ctx => ctx.Level >= 5,
            Progress = ctx => Math.Min((double)ctx.Level / 5, 1.0)
        },

        // Perfect day
        new() {
            Key = "perfect_day",
            Name = "Ідеальний день",
            Description = "Виконай всі звички за один день",
            Icon = "✨",
            Trigger = BadgeTrigger.OnHabitCompleted,
            Condition = ctx => ctx.AllTodayHabitsCompleted,
            Progress = ctx => ctx.TodayCompletionRate
        },
        new() {
            Key = "perfect_week",
            Name = "Ідеальний тиждень",
            Description = "Виконай всі звички 7 днів поспіль",
            Icon = "🎯",
            Trigger = BadgeTrigger.OnStreakUpdated,
            Condition = ctx => ctx.PerfectDaysStreak >= 7,
            Progress = ctx => Math.Min((double)ctx.PerfectDaysStreak / 7, 1.0)
        },
    };

        public static BadgeDefinition? GetByKey(string key) =>
            All.FirstOrDefault(b => b.Key == key);
    }
}

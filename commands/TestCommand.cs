using System.ComponentModel;
using System.Globalization;
using DSharpPlus.Commands;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Entities;
using TUDU_BOT.Classes;

namespace TUDU_BOT.commands
{
    public class TestCommand
    {
        // Give the command a name
        [Command("test")]
        public async Task MyFirstCommand(CommandContext ctx)
        {
            var tasks = TaskManager.GetTasks(ctx.User.Id);
            if (tasks.Count == 0)
            {
                await ctx.RespondAsync("📭 You have no tasks!");
                return;
            }

            var embed = new DiscordEmbedBuilder()
                .WithTitle($"{ctx.User.Username}'s To-Do List")
                .WithColor(DiscordColor.Blurple);

            // Add each task to the embed
            for (int i = 0; i < tasks.Count; i++)
            {
                string status = tasks[i].IsCompleted ? "✅" : "❌";
                string dueDateStr = tasks[i].DueDate == DateTime.MaxValue ? "No due date" : tasks[i].DueDate.ToString("dd/MM/yyyy");

                // Add task info to embed
                embed.AddField($"Task {i + 1}", $"{status} {tasks[i].Description} (Due: {dueDateStr})", false);
            }

            await ctx.RespondAsync(embed);
        }

        [Command("addtask")]
        public async Task AddTask(CommandContext ctx, string description, [Description("FORMAT DD/MM/YYYY")] string date)
        {
            DateTime dueDate;

            if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dueDate))
            {
                await ctx.RespondAsync("❌ Invalid date format. Please use dd/MM/yyyy.");
                return;
            }

            TaskManager.AddTask(ctx.User.Id, description, dueDate);
            await ctx.RespondAsync($"📝 Task added: {description} (Due: {dueDate.ToString("dd/MM/yyyy")})");
        }

        [Command("remove")]
        public async Task RemoveTask(CommandContext ctx, int index)
        {
            TaskManager.RemoveTask(ctx.User.Id, index);
            await ctx.RespondAsync($"🗑️ Task removed.");
        }

        [Command("cleartasks")]
        public async Task RemoveAllTask(CommandContext ctx)
        {
            TaskManager.RemoveAllTask(ctx.User.Id);
            await ctx.RespondAsync("🗑️ All tasks have been removed.");
        }

        [Command("todo")]
        public async Task ListTasks(CommandContext ctx)
        {
            var tasks = TaskManager.GetTasks(ctx.User.Id);
            if (tasks.Count == 0)
            {
                await ctx.RespondAsync("📭 You have no tasks!");
                return;
            }

            var embed = new DiscordEmbedBuilder()
                .WithTitle($"{ctx.User.Username}'s To-Do List")
                .WithColor(DiscordColor.Blurple);

            // Add each task to the embed
            for (int i = 0; i < tasks.Count; i++)
            {
                string status = tasks[i].IsCompleted ? "✅" : "❌";
                string dueDateStr = tasks[i].DueDate == DateTime.MaxValue ? "No due date" : tasks[i].DueDate.ToString("dd/MM/yyyy");
                embed.AddField($"Task {i + 1}", $"{status} {tasks[i].Description} (Due: {dueDateStr})", false);
            }

            await ctx.RespondAsync(embed);
        }

        [Command("complete")]
        public async Task CompleteTask(CommandContext ctx, int index)
        {
            if (TaskManager.CompleteTask(ctx.User.Id, index - 1))
                await ctx.RespondAsync("✅ Task marked as completed!");
            else
                await ctx.RespondAsync("❌ Invalid task number.");
        }

        [Command("uncheck")]
        public async Task UnCheckTask(CommandContext ctx, int index)
        {
            if (TaskManager.UnCheckTask(ctx.User.Id, index - 1))
                await ctx.RespondAsync("Task has been unmarked!");
            else
                await ctx.RespondAsync("❌ Invalid task number.");
        }

        [Command("edittask")]
        public async Task EditTask(CommandContext ctx, int index, [RemainingText] string newDescription)
        {
            if (TaskManager.EditTask(ctx.User.Id, index - 1, newDescription))
                await ctx.RespondAsync("✅ Task updated!");
            else
                await ctx.RespondAsync("❌ Invalid task number.");
        }
    }
}

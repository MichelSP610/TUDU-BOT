using System.ComponentModel;
using System.Globalization;
using DSharpPlus.Commands;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Commands.ContextChecks;
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
            var member = ctx.Member;

            if (member == null || !member.Roles.Any(role => role.Name == "ADMIN"))
            {
                await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("❌ You don't have permission to use this command.")
                    .AsEphemeral());
                return;
            }

            var tasks = TaskManager.GetTasks(ctx.User.Id);

            if (tasks.Count == 0)
            {
                await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("📭 You have no tasks!")
                    .AsEphemeral());
                return;
            }

            var embed = new DiscordEmbedBuilder()
                .WithTitle($"{ctx.User.Username}'s To-Do List")
                .WithColor(DiscordColor.Blurple);

            for (int i = 0; i < tasks.Count; i++)
            {
                string status = tasks[i].IsCompleted ? "✅" : "❌";
                string dueDateStr = tasks[i].DueDate == DateTime.MaxValue ? "No due date" : tasks[i].DueDate.ToString("dd/MM/yyyy");
                embed.AddField($"Task {i + 1}", $"{status} {tasks[i].Description} (Due: {dueDateStr})", false);
            }

            await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                .AddEmbed(embed)
                .AsEphemeral());
        }

        [Command("addtask")]
        public async Task AddTask(CommandContext ctx, string description, [Description("FORMAT DD/MM/YYYY")] string date)
        {
            if (!DateTime.TryParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dueDate))
            {
                await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("❌ Invalid date format. Please use dd/MM/yyyy.")
                    .AsEphemeral());
                return;
            }

            TaskManager.AddTask(ctx.User.Id, description, dueDate);
            await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                .WithContent($"📝 Task added: {description} (Due: {dueDate:dd/MM/yyyy})")
                .AsEphemeral());
        }

        [Command("remove")]
        public async Task RemoveTask(CommandContext ctx, int index)
        {
            TaskManager.RemoveTask(ctx.User.Id, index);
            await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                .WithContent("🗑️ Task removed.")
                .AsEphemeral());
        }

        [Command("cleartasks")]
        public async Task ClearAllTasks(CommandContext ctx)
        {
            TaskManager.RemoveAllTask(ctx.User.Id);
            await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                .WithContent("🗑️ All tasks have been removed.")
                .AsEphemeral());
        }

        [Command("todo")]
        public async Task ListTasks(CommandContext ctx)
        {
            var tasks = TaskManager.GetTasks(ctx.User.Id);

            if (tasks.Count == 0)
            {
                await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("📭 You have no tasks!")
                    .AsEphemeral(true)); // private
                return;
            }

            var embed = new DiscordEmbedBuilder()
                .WithTitle($"{ctx.User.Username}'s To-Do List")
                .WithColor(DiscordColor.Blurple);

            for (int i = 0; i < tasks.Count; i++)
            {
                string status = tasks[i].IsCompleted ? "✅" : "❌";
                string dueDateStr = tasks[i].DueDate == DateTime.MaxValue ? "No due date" : tasks[i].DueDate.ToString("dd/MM/yyyy");
                embed.AddField($"Task {i + 1}", $"{status} {tasks[i].Description} (Due: {dueDateStr})", false);
            }

            await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                .AddEmbed(embed)
                .AsEphemeral(true)); // private
        }

        [Command("complete")]
        public async Task CompleteTask(CommandContext ctx, int index)
        {
            if (TaskManager.CompleteTask(ctx.User.Id, index - 1))
                await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("✅ Task marked as completed!")
                    .AsEphemeral());
            else
                await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("❌ Invalid task number.")
                    .AsEphemeral());
        }

        [Command("uncheck")]
        public async Task UnCheckTask(CommandContext ctx, int index)
        {
            if (TaskManager.UnCheckTask(ctx.User.Id, index - 1))
                await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("🔁 Task has been unmarked!")
                    .AsEphemeral());
            else
                await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("❌ Invalid task number.")
                    .AsEphemeral());
        }

        [Command("edittask")]
        public async Task EditTask(CommandContext ctx, int index, [RemainingText] string newDescription)
        {
            if (TaskManager.EditTask(ctx.User.Id, index - 1, newDescription))
                await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("✅ Task updated!")
                    .AsEphemeral());
            else
                await ctx.RespondAsync(new DiscordInteractionResponseBuilder()
                    .WithContent("❌ Invalid task number.")
                    .AsEphemeral());
        }
    }
}

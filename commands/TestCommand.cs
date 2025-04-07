using DSharpPlus.Commands;
using DSharpPlus.Commands.ArgumentModifiers;
using DSharpPlus.Entities;
using TUDU_BOT.Classes;

namespace TUDU_BOT.commands
{
    public class TestCommand
    {
        //Give the command a name
        [Command("test")]
        public async Task MyFirstCommand(CommandContext ctx) //EVERY COMMAND NEEDS CommandContext!!!
        {

            var message = new DiscordMessageBuilder()
            .AddEmbed(new DiscordEmbedBuilder()
                .WithColor(DiscordColor.Red)
                .WithAuthor("Jesus")
                .WithTitle("test"))
            .AddComponents(new DiscordComponent[]
            {
                new DiscordButtonComponent(DiscordButtonStyle.Primary, "1_top", "Blurple!"),
                new DiscordButtonComponent(DiscordButtonStyle.Secondary, "2_top", "Grey!"),
                new DiscordButtonComponent(DiscordButtonStyle.Success, "3_top", "Green!"),
                new DiscordButtonComponent(DiscordButtonStyle.Danger, "4_top", "Red!"),
                new DiscordLinkButtonComponent("https://google.com", "Link!")
            });

            await ctx.RespondAsync(message);
        }

        [Command("addtask")]
        public async Task AddTask(CommandContext ctx, [RemainingText] string description)
        {
            TaskManager.AddTask(ctx.User.Id, description);
            await ctx.RespondAsync($"📝 Task added: {description}");
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

            string response = "🗒️ Your Tasks:\n";
            for (int i = 0; i < tasks.Count; i++)
            {
                response += $"{i + 1}. {tasks[i]}\n";
            }

            await ctx.RespondAsync(response);
        }

        [Command("complete")]
        public async Task CompleteTask(CommandContext ctx, int index)
        {
            if (TaskManager.CompleteTask(ctx.User.Id, index - 1))
                await ctx.RespondAsync("✅ Task marked as completed!");
            else
                await ctx.RespondAsync("❌ Invalid task number.");
        }



    }
}

using System.Threading.Tasks;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using TUDU_BOT.other;

namespace TUDU_BOT.commands
{
    public class TestCommand
    {
        //Give the command a name
        [Command("test")]
        public async Task MyFirstCommand(CommandContext ctx) //EVERY COMMAND NEEDS CommandContext!!!
        {
            await ctx.Channel.SendMessageAsync($"Tus muertos {ctx.User.Username}, eres subnormal");
        } 

        [Command("add")]
        public async Task Add(CommandContext ctx, int number1, int number2)
        {
            int resoult = number1 + number2;
            await ctx.Channel.SendMessageAsync($"{resoult}");
        }

        //How to make a embed message
        [Command("embed2")]
        public async Task EmbedMessageEz(CommandContext ctx)
        {
            var message = new DiscordEmbedBuilder
            {
                Title = " VIVA CRISTO REY ",
                Description = " Ghillerme MARICOOON ",
                Color = DiscordColor.Red,
            };
            await ctx.Channel.SendMessageAsync(embed: message); //SEND 'embed: message' so there is no overload with this method.
        }

        //Small random card generator Game.
        [Command("cardgame")]
        public async Task CardGame(CommandContext ctx)
        {
            var userCard = new CardSystem();

            var userCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"YOUR CARD is {userCard.selectedCard}",
                Color = DiscordColor.Black
            };

            await ctx.Channel.SendMessageAsync (embed: userCardEmbed);

            var botCard = new CardSystem();

            var botCardEmbed = new DiscordEmbedBuilder
            {
                Title = $"BOT's CARD IS {botCard.selectedCard}",
                Color = DiscordColor.Red
            };

            await ctx.Channel.SendMessageAsync(embed: botCardEmbed);

            if (userCard.selectedNumber > botCard.selectedNumber)
            {
                //User Wins
                var userWinEmbed = new DiscordEmbedBuilder
                {
                    Title = "YOU WIN!!!",
                    Color = DiscordColor.Blue
                };
                await ctx.Channel.SendMessageAsync(embed: userWinEmbed);
            } else
            {
                //Bot wins
                var botWinEmbed = new DiscordEmbedBuilder
                {
                    Title = "YOU LOSE :(",
                    Color = DiscordColor.Blue
                };
                await ctx.Channel.SendMessageAsync(embed: botWinEmbed);
            }
        }
    }
}

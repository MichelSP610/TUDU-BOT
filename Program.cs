

using DSharpPlus.Commands.Processors.SlashCommands;
using DSharpPlus.Commands.Processors.TextCommands;
using DSharpPlus.Commands;
using DSharpPlus.Entities;
using DSharpPlus;
using TUDU_BOT.config;
using Newtonsoft.Json;
using TUDU_BOT.commands;
using DSharpPlus.Commands.Processors.TextCommands.Parsing;
using DSharpPlus.EventArgs;

namespace TUDU_BOT
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //Start reading the json with the bot token.
            var jsonReader = new JSONReader();
            await jsonReader.ReadJSON();

            string discordToken = jsonReader.token;

            if (string.IsNullOrWhiteSpace(discordToken))
            {
                Console.WriteLine("Error: No discord token found. Please provide a token via the DISCORD_TOKEN environment variable.");
                Environment.Exit(1);
            }

            DiscordClientBuilder builder = DiscordClientBuilder.CreateDefault(discordToken, TextCommandProcessor.RequiredIntents | SlashCommandProcessor.RequiredIntents);

            builder.UseCommands((IServiceProvider serviceProvider, CommandsExtension extension) =>
            {
                extension.AddCommands([typeof(TestCommand) /* Añadir mas classes de comandos */]);
                TextCommandProcessor textCommandProcessor = new(new()
                {
                    // The default behavior is that the bot reacts to direct
                    // mentions and to the "!" prefix. If you want to change
                    // it, you first set if the bot should react to mentions
                    // and then you can provide as many prefixes as you want.
                    PrefixResolver = new DefaultPrefixResolver(true, jsonReader.prefix).ResolvePrefixAsync,
                });

                // Add text commands with a custom prefix (?ping)
                extension.AddProcessor(textCommandProcessor);
            }).ConfigureEventHandlers
            (
                b => b.HandleGuildDownloadCompleted(GuildDownloadCompleted)
            );

            DiscordClient client = builder.Build();

            DiscordActivity status = new("SEX WITH HITLER", DiscordActivityType.Playing);


            //Connect the bot to the discord servers, and makes the bot stay online while the program is running.
            await client.ConnectAsync(status, DiscordUserStatus.Online);
            await Task.Delay(-1);
        }

        private static async Task GuildDownloadCompleted(DiscordClient client, GuildDownloadCompletedEventArgs args)
        {
            Console.WriteLine("TUDU ready");
        }


    }
}

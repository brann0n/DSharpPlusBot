using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Enums;
using DSharpPlusBot.Commands;
using DSharpPlusBot.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace DSharpPlusBot
{
    public class Bot
    {
        private DiscordClient _client;
        private CommandsNextExtension _commands;
        private InteractivityExtension _interactivity;
        private Config _config;

        public async Task RunAsync()
        {
            if (File.Exists("config.json"))
            {
                _config = JObject.Parse(File.ReadAllText("config.json")).ToObject<Config>();
            }
            else
            {
                File.Create("config.json").Close();
                File.WriteAllText("config.json", JObject.FromObject(new Config()).ToString());
                Console.WriteLine("Created a new config file. Please write out all values");
                Console.ReadKey();
                Environment.Exit(0);
            }

            this._client = new DiscordClient(new DiscordConfiguration()
            {
                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                Token = _config.Token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true
            });

            this._interactivity = this._client.UseInteractivity(new InteractivityConfiguration()
            {
                PaginationBehaviour = PaginationBehaviour.WrapAround,
                PaginationDeletion = PaginationDeletion.DeleteEmojis,
                PaginationEmojis = new PaginationEmojis(),
                PollBehaviour = PollBehaviour.DeleteEmojis,
                Timeout = TimeSpan.FromSeconds(45) // timeout op interactivity
            });

            var services = new ServiceCollection()
                .AddSingleton<DiscordClient>(_client)
                .AddSingleton<InteractivityExtension>(_interactivity)
                .AddSingleton<Config>(_config)
                .AddSingleton<Bot>(this)
                .BuildServiceProvider();

            this._commands = this._client.UseCommandsNext(new CommandsNextConfiguration()
            {
                StringPrefixes = new List<string> { _config.CommandPrefix },
                EnableDms = false, // geen commands via DM, kan je aanpassen als je wil
                EnableDefaultHelp = true, // zelf geen help maken, vet handig
                EnableMentionPrefix = true, // @bot doe dingen, vet leuk
                CaseSensitive = false, // @bOt dOe dINgEn lekker sarcastisch
                Services = services
            });

            _commands.RegisterCommands<MainCommands>();
            _commands.RegisterCommands<OwnerCommands>();

            await _client.ConnectAsync(new DSharpPlus.Entities.DiscordActivity("hentai", DSharpPlus.Entities.ActivityType.Watching));
        }
    }
}

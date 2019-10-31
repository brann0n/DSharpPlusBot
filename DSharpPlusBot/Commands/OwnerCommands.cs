﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using DSharpPlusBot.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpPlusBot.Commands
{
    [RequireOwner] // alleen owner kan dit.
    public class OwnerCommands : BaseCommandModule
    {
        private DiscordClient _client;
        private InteractivityExtension _interactivity;
        private Config _config;
        private Bot _bot;

        public OwnerCommands(InteractivityExtension interactivity, Config cfg, DiscordClient client, Bot bot)
        {
            _interactivity = interactivity;
            _config = cfg;
            _client = client;
            _bot = bot;
            // CommandsNextExtension kan niet opgehaald worden via dependency injection
            // maar CommandContext heeft een CommandsNext property die de extension wel heeft.
        }

        [Command("owner")]
        public async Task PingAsync(CommandContext ctx)
        {
            await ctx.RespondAsync($"You're owner");
        }
    }
}

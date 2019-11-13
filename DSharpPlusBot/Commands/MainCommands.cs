using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using DSharpPlusBot.Entities;
using DSharpPlusBot.Modules;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DSharpPlusBot.Commands
{
	public class MainCommands : BaseCommandModule
	{
		private DiscordClient _client;
		private InteractivityExtension _interactivity;
		private Config _config;
		private Bot _bot;

		public MainCommands(InteractivityExtension interactivity, Config cfg, DiscordClient client, Bot bot)
		{
			_interactivity = interactivity;
			_config = cfg;
			_client = client;
			_bot = bot;
			// CommandsNextExtension kan niet opgehaald worden via dependency injection
			// maar CommandContext heeft een CommandsNext property die de extension wel heeft.
		}

		[Command("ping")]
		public async Task PingAsync(CommandContext ctx)
		{
			await ctx.RespondAsync($"Pong! {_client.Ping} ms");
		}

		[Command("repeat")]
		public async Task PingAsync(CommandContext ctx, [RemainingText]string text)
		{
			await ctx.TriggerTypingAsync();
			var embed = new DiscordEmbedBuilder();

			embed.WithAuthor(ctx.User.Username, null, ctx.User.AvatarUrl);
			embed.WithDescription(text);
			await ctx.RespondAsync(embed: embed);
		}

		[Command("agree")]
		public async Task AgreeAsync(CommandContext ctx)
		{
			await ctx.Message.DeleteAsync();

			await ctx.TriggerTypingAsync();

			var embed = new DiscordEmbedBuilder
			{
				ImageUrl = "https://i.ytimg.com/vi/WHEdoFXUnYQ/maxresdefault.jpg",

			};
			embed.WithAuthor(ctx.User.Username, null, ctx.User.AvatarUrl);


			await ctx.RespondAsync(embed: embed);
		}

		[Command("kick")]
		public async Task KickSytze(CommandContext ctx, DiscordMember usr)
		{
			try
			{

				MemoryStream strm = await ImageManipulation.ProcessImageAsync(ctx.Member.Nickname ?? ctx.User.Username, usr.Nickname ?? usr.Username);
				strm.Position = 0;
				await ctx.RespondWithFileAsync("kicking.png", strm);
			}
			catch (Exception exce)
			{
				await ctx.RespondAsync("Dikke error chappie, geen image voor jou");
			}

		}
	}
}

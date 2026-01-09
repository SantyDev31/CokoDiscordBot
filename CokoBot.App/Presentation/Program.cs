using CokoBot.App.Application.Dispactcher;
using CokoBot.App.Application.Handlers;
using CokoBot.App.Domain.Interfaces;
using CokoBot.App.Infrastructure.Welcome;
using CokoBot.App.Presentation.Commands;
using CokoBot.App.Presentation.Triggers;
using CokoBot.Core.Configuration;
using CokoBot.DailySong.Infrastructure.Scheduling;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.DependencyInjection;

namespace CokoBot.App.Presentation
{
    public static class Program
    {
        public static DiscordClient DClient;
        public static Random random = new Random();

        private static CommandHandler commandsHandler = Startup.ServiceProvider.GetRequiredService<CommandHandler>();
        private static TriggerDispatcher triggerDispatcher = Startup.ServiceProvider.GetRequiredService<TriggerDispatcher>();

        private static readonly BotSettings botSettings = Startup.AppSettings.BotSettings;

        public static async Task Main()
        {
            DClient = new DiscordClient(new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = botSettings.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LoggerFactory = Startup.ILoggerFactory,
            });

            botSettings.Token = "mitemite";

            DClient.Ready += OnBotReady;
            DClient.MessageCreated += OnMessageCreated;
            DClient.GuildMemberAdded += OnGuildMemberAdded;

            Console.CancelKeyPress += async (_, e) =>
            {
                e.Cancel = true;
                await DClient.DisconnectAsync();
                Environment.Exit(0);
            };

            await DClient.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task OnBotReady(DiscordClient sender, ReadyEventArgs @event)
        {
            var timer = Startup.ServiceProvider.GetRequiredService<DailyTimer>();
            timer.Start();
            return Task.CompletedTask;
        }

        private static async Task OnMessageCreated(DiscordClient sender, MessageCreateEventArgs @event)
        {
            if (@event.Author.IsBot) return;
            var msg = @event.Message.Content.ToLower();
            
            await commandsHandler.ExecuteAsync(msg, @event);
            
            await triggerDispatcher.DispatchAsync(msg, sender, @event);
        }

        private static async Task OnGuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs @event)
        {
            DiscordChannel channel = await DClient.GetChannelAsync(1424036843334139961);
            await NewMemberIMG.CreateWelcomeMessage(channel, @event.Member.AvatarUrl, @event.Member.DisplayName);
        }
    }
}

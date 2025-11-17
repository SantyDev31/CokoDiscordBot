using CokoBot.App.Commands;
using CokoBot.App.Triggers;
using CokoBot.DailySong.Services;
using DSharpPlus;
using DSharpPlus.EventArgs;

namespace CokoBot.App
{
    public static class Program
    {
        public static DiscordClient DClient;
        private static DailyCokoTimer _dailyTask;
        public static Random random = new Random();

        private static readonly SimplePrefixCommands commands = new SimplePrefixCommands();
        private static readonly CommandHandler commandsHandler = new CommandHandler(commands);

        private static readonly List<ITrigger> Triggers = new()
        {
            new CokoPanTrigger(),
            new CokoGunTrigger(),
            new CokoNerdgeTrigger(),
            new CokoYesNoTrigger(),
        };

        public static async Task Main()
        {
            DClient = new DiscordClient(new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = Startup.AppSettings.BotSettings.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                LoggerFactory = Startup.ILoggerFactory,
            });

            Startup.AppSettings.BotSettings.Token = "mitemite";

            DClient.Ready += OnBotReady;
            DClient.MessageCreated += OnMessageCreated;

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
            _dailyTask = new DailyCokoTimer(DailyCoko.SendSong);
            _dailyTask.Start();
            return Task.CompletedTask;
        }

        private static async Task OnMessageCreated(DiscordClient sender, MessageCreateEventArgs @event)
        {
            if (@event.Author.IsBot) return;
            var msg = @event.Message.Content.ToLower();

            commandsHandler.Execute(msg, msg, @event);

            var trigger = Triggers.FirstOrDefault(t => t.Matches(msg));
            if (trigger != null)
            {
                await trigger.Execute(sender, @event);
            }
        }
    }
}

using CokoBot.App.Application.Commands.Attributes;
using CokoBot.App.Domain.Interfaces;
using CokoBot.App.Infrastructure.Welcome;
using CokoBot.DailySong.Application.Ports;
using CokoBot.DailySong.Domain.Entities;
using CokoBot.DailySong.Infrastructure.Scheduling;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CokoBot.App.Presentation.Commands
{
    public class DebugCommands : ICommandModule
    {
        private static ulong _ownerId = Startup.AppSettings.BotSettings.OwnerId;

        [Command("test")]
        public async Task TestCommand(string msg, MessageCreateEventArgs @event)
        {
            await @event.Channel.SendMessageAsync("Test Works");
        }
        [Command("testsong")]
        public async Task TestSongCommand(string msg, MessageCreateEventArgs @event)
        {
            if (await OnlyOwnerCommad(msg, @event))
            {
                return;
            }
            await Startup.ServiceProvider.GetRequiredService<DailyJob>().ExecuteAsync();
        }

        [Command("welcometest")]
        public async Task TestWelcomeCommand(string msg, MessageCreateEventArgs @event)
        {
            await NewMemberIMG.CreateWelcomeMessage(@event.Channel, @event.Author.AvatarUrl, @event.Author.Username);
        }
        private static async Task<bool> OnlyOwnerCommad(string msg, MessageCreateEventArgs @event)
        {
            if (@event.Message.Author.Id != _ownerId)
            {
                await @event.Channel.SendMessageAsync("Only my true master can use that command");
                return true;
            }
            return false;
        }
    }
}

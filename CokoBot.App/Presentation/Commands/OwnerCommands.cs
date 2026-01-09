using CokoBot.App.Application.Commands.Attributes;
using CokoBot.App.Domain.Interfaces;
using CokoBot.Core.Configuration;
using CokoBot.DailySong.Infrastructure.Scheduling;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.DependencyInjection;

namespace CokoBot.App.Presentation.Commands
{
    public class OwnerCommands : ICommandModule
    {
        private static ulong _ownerId = Startup.AppSettings.BotSettings.OwnerId;

        [Command("forgor")]
        public async Task ForgotCommand(string msg, MessageCreateEventArgs @event)
        {
            if (await OnlyOwnerCommad(msg, @event))
            {
                return;
            }
            await @event.Channel.SendMessageAsync("マスター…テスト終わったあと、毎回設定し直すの忘れちゃダメなんだよ？");
            await Startup.ServiceProvider.GetRequiredService<DailyJob>().ExecuteAsync();
        }

        [Command("msg")]
        public async Task ShirokoUsesCokoToSendMessage(string msg, MessageCreateEventArgs @event)
        {
            if (await OnlyOwnerCommad(msg, @event))
            {
                return;
            }
            string[] split = msg.Split("|", StringSplitOptions.RemoveEmptyEntries);
            try
            {
                ulong channelId = ulong.Parse(split[0]);
                DiscordChannel channel = await Program.DClient.GetChannelAsync(channelId);
                await channel.SendMessageAsync(split[1]);
            }
            catch (Exception)
            {
                return;
            }
            await @event.Message.DeleteAsync();
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

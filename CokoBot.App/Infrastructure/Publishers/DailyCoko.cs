using CokoBot.App.Presentation;
using CokoBot.Core.Templates;
using CokoBot.DailySong.Application.Ports;
using CokoBot.DailySong.Domain.Entities;

namespace CokoBot.App.Infrastructure.Publishers
{
    public class DailyCoko : IDailySongPublisher
    {
        public async Task PublishAsync(CokoSong song)
        {
            ulong channelId = Startup.AppSettings.BotSettings.DailyCokoChannel;

            var channel = await Program.DClient.GetChannelAsync(channelId);

            await channel.SendMessageAsync(
                Templates.DailyCokoMessage(
                    song.songName,
                    song.songURL,
                    song.songType,
                    song.userName,
                    song.userURL
                )
            );
        }
    }
}

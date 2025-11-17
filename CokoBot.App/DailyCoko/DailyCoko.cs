using CokoBot.Core.Templates;
using CokoBot.DailySong.Data;
using CokoBot.DailySong.Models;

namespace CokoBot.App
{
    public class DailyCoko
    {
        public static async Task SendSong()
        {
            //CHANNEL ID FOR THE DAILY RECOMMENDATION
            ulong channelId = 0;

            var channel = await Program.DClient.GetChannelAsync(channelId);

            CokoSong song = await CokoSongConnection.SendDailyCoko();
            await channel.SendMessageAsync(Templates.DailyCokoMessage(song.songName, song.songURL, song.songType, song.userName, song.userURL));
        }

    }
}

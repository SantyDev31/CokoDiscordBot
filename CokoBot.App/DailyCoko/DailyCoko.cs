using CokoBot.Core.Templates;
using CokoBot.DailySong.Data;
using CokoBot.DailySong.Models;

namespace CokoBot.App
{
    public class DailyCoko
    {
        public static async Task SendSong()
        {
            ulong channelId = //Here goes the channel you want to have for the daily recommendations;

            var channel = await Program.DClient.GetChannelAsync(channelId);

            CokoSong song = await CokoSongConnection.SendDailyCoko();
            await channel.SendMessageAsync(Templates.DailyCokoMessage(song.songName, song.songURL, song.songType, song.userName, song.userURL));
        }

    }
}

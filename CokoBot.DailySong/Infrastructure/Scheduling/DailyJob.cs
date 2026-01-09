using CokoBot.DailySong.Application.Ports;
using CokoBot.DailySong.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.DailySong.Infrastructure.Scheduling
{
    public class DailyJob
    {
        private readonly GetRandomSong _getRandomSong;
        private readonly IDailySongPublisher _publisher;

        public DailyJob(GetRandomSong getRandomSong, IDailySongPublisher publisher)
        {
            _getRandomSong = getRandomSong;
            _publisher = publisher;
        }

        public async Task ExecuteAsync()
        {
            var song = await _getRandomSong.ExecuteAsync();
            await _publisher.PublishAsync(song);
        }
    }
}

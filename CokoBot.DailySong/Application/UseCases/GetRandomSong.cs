using CokoBot.DailySong.Domain.Entities;
using CokoBot.DailySong.Domain.Interfaces;

namespace CokoBot.DailySong.Application.UseCases
{
    public class GetRandomSong
    {
        private readonly ICokoSongRepository _repository;

        public GetRandomSong(ICokoSongRepository repository)
        {
            _repository = repository;
        }

        public async Task<CokoSong> ExecuteAsync()
        {
            var song = await _repository.GetRandomAsync();

            if (song == null)
            {
                await _repository.ResetAllRecommendations();
                song = await _repository.GetRandomAsync();
            }

            await _repository.MarkAsRecommended(song.id);
            return song;
        }
    }
}

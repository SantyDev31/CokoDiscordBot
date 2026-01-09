using CokoBot.DailySong.Domain.Entities;

namespace CokoBot.DailySong.Application.Ports
{
    public interface IDailySongPublisher
    {
        Task PublishAsync(CokoSong song);
    }
}

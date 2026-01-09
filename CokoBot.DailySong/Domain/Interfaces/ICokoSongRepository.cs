using CokoBot.DailySong.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.DailySong.Domain.Interfaces
{
    public interface ICokoSongRepository
    {
        Task<CokoSong?> GetRandomAsync();
        Task MarkAsRecommended(int id);
        Task ResetAllRecommendations();
    }
}

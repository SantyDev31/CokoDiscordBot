using CokoBot.DailySong.Application.UseCases;
using CokoBot.DailySong.Domain.Interfaces;
using CokoBot.DailySong.Infrastructure.Persistence;
using CokoBot.DailySong.Infrastructure.Scheduling;
using Microsoft.Extensions.DependencyInjection;


namespace CokoBot.DailySong.Installers
{
    public static class DailySongInstaller
    {
        public static IServiceCollection AddDailySongModule(this IServiceCollection services, string dbPath)
        {
            services.AddScoped<ICokoSongRepository>(_ => new CokoSongRepository(dbPath));
            services.AddScoped<GetRandomSong>();
            services.AddScoped<DailyJob>();

            services.AddSingleton<DailyTimer>();

            return services;
        }
    }

}

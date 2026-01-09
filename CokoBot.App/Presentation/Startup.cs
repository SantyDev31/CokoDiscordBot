using CokoBot.App.Application.Dispactcher;
using CokoBot.App.Application.Triggers;
using CokoBot.App.Infrastructure.Configuration.Triggers;
using CokoBot.App.Infrastructure.Installers;
using CokoBot.App.Infrastructure.Publishers;
using CokoBot.Core.Configuration;
using CokoBot.DailySong.Application.Ports;
using CokoBot.DailySong.Installers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CokoBot.App.Presentation
{
    public static class Startup
    {
        private static readonly IConfiguration IConfiguration = LoadConfiguration();
        public static readonly AppSettings AppSettings = IConfiguration.Get<AppSettings>();

        private static IConfiguration LoadConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddUserSecrets(typeof(Startup).Assembly)
                .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "Infrastructure", "Configuration", "config.json"), optional: false, reloadOnChange: true);

            return builder.Build();
        }

        public static readonly ILoggerFactory ILoggerFactory = LoggerFactoryConf(IConfiguration);
        private static ILoggerFactory LoggerFactoryConf(IConfiguration IConfiguration)
        {
            return LoggerFactory.Create(builder =>
            {
                builder.AddConfiguration(IConfiguration);
                builder
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddFilter("Microsoft", LogLevel.Trace)
                    .AddFilter("System", LogLevel.Trace)
                    .AddFilter("LoggingConsoleApp.Program", LogLevel.Trace)
                    .AddConsole()
                    //.AddEventLog()
                    ;
            });
        }

        // Dependence Injection
        public static readonly IServiceProvider ServiceProvider = ConfigureServices();
        private static IServiceProvider ConfigureServices()
        {
            ServiceCollection services = new ServiceCollection();

            services.AddSingleton(IConfiguration);
            services.AddSingleton(ILoggerFactory);

            services.AddDailySongModule(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "CokoBot.DailySong", "Infrastructure", "Persistence", "songs.db"));
            services.AddSingleton<IDailySongPublisher, DailyCoko>();

            services.AddSingleton<ITriggerConfigFactory, TriggerConfigFactory>();
            services.AddSingleton<TriggerDispatcher>();

            services.AddCommands();
            services.AddTriggers();


            services.AddLogging();
            return services.BuildServiceProvider();
        }
    }
}

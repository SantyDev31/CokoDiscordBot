using System;
using CokoBot.Core.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CokoBot.App
{
    public static class Startup
    {
        private static readonly IConfiguration IConfiguration = LoadConfiguration();
        public static readonly AppSettings AppSettings = IConfiguration.Get<AppSettings>();

        private static IConfiguration LoadConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder();
            builder = builder.AddJsonFile(Path.Combine(AppContext.BaseDirectory, "config.json"), optional: false, reloadOnChange: true);
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

            services.AddLogging();
            return services.BuildServiceProvider();
        }
    }
}

using CokoBot.App.Application.Handlers;
using CokoBot.App.Domain.Interfaces;
using CokoBot.App.Presentation.Commands;
using CokoBot.App.Presentation.Commands.MiniGames;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Infrastructure.Installers
{
    public static class CommandInstaller
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddSingleton<ICommandModule,DebugCommands>();
            services.AddSingleton<ICommandModule,OwnerCommands>();
            services.AddSingleton<ICommandModule,FunCommands>();

            services.AddSingleton<ICommandModule, HangManCommands>();


            services.AddSingleton<CommandHandler>();

            return services;
        }
    }
}

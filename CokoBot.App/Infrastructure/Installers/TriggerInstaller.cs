using CokoBot.App.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Infrastructure.Installers
{
    public static class TriggerInstaller
    {
        public static IServiceCollection AddTriggers(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<ITrigger>()
                .AddClasses(c => c.AssignableTo<ITrigger>())
                .As<ITrigger>()
                .WithSingletonLifetime());

            return services;
        }
    }
}

using CokoBot.App.Application.Commands;
using CokoBot.App.Application.Commands.Attributes;
using CokoBot.App.Domain.Interfaces;
using CokoBot.App.Presentation;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CokoBot.App.Application.Handlers
{
    public class CommandHandler
    {
        private readonly IServiceProvider _services;
        private readonly string _prefix;

        public CommandHandler(IServiceProvider services)
        {
            _services = services;
            _prefix = Startup.AppSettings.BotSettings.Prefix;
        }

        public async Task<bool> ExecuteAsync(string message, MessageCreateEventArgs e)
        {
            if (!message.StartsWith(_prefix))
                return false;

            var content = message[_prefix.Length..];
            var split = content.Split(' ', 2);
            var commandName = split[0];
            var args = split.Length > 1 ? split[1] : string.Empty;

            var modules = _services.GetServices<ICommandModule>();

            foreach (var module in modules)
            {
                var method = module.GetType().GetMethods().FirstOrDefault(m => m.GetCustomAttribute<CommandAttribute>()?.Name == commandName);

                if (method != null)
                {
                    await (Task)method.Invoke(module, [args, e]);
                    return true;
                }
            }

            return false;
        }
    }
}

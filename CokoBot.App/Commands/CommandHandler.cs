using DSharpPlus.EventArgs;
using System.Linq;
using System.Reflection;

namespace CokoBot.App.Commands
{
    public class CommandHandler
    {
        private readonly object _instance;
        string prefix = Startup.AppSettings.BotSettings.Prefix;
        public CommandHandler(object instance)
        {
            _instance = instance;
        }
        public bool Execute(string commandMsg, params object[] commandArguments)
        {
            if (!commandMsg.StartsWith(prefix))
            {
                return false;
            }

            commandArguments[0] = string.Join(" ",commandMsg.Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).ToArray());

            var commandMethod = _instance.GetType().GetMethods().FirstOrDefault(c =>
            {
                var attribute = c.GetCustomAttribute<CommandAttribute>();
                return attribute != null && commandMsg.StartsWith($"{Startup.AppSettings.BotSettings.Prefix}{attribute.Name}");
            });
            if (commandMethod != null)
            {
                commandMethod.Invoke(_instance, commandArguments);
                return true;
            }
            return false;
        }
    }
}

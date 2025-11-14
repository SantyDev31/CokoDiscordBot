using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Triggers
{
    public class CokoYesNoTrigger : ITrigger
    {
        private readonly string[] _keywords = Startup.AppSettings.BotSettings.Triggers.CokoYesNoTriggers;
        public bool Matches(string message)
        {
            return _keywords.Any(k => message.StartsWith(k));
        }
        public async Task Execute(DiscordClient client, MessageCreateEventArgs @event)
        {
            if(Program.random.Next(0, 2) == 0)
            {
                await @event.Channel.SendMessageAsync($"Yes");
            }
            else
            {
                await @event.Channel.SendMessageAsync($"No");
            }
        }
    }
}

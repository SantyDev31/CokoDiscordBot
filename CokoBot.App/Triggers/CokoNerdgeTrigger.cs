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
    public class CokoNerdgeTrigger : ITrigger
    {
        private readonly string[] _keywords = Startup.AppSettings.BotSettings.Triggers.CokoNerdgeTriggers;
        public bool Matches(string message)
        {
            return _keywords.Any(k => message.StartsWith(k));
        }
        public async Task Execute(DiscordClient client, MessageCreateEventArgs @event)
        {
            DiscordEmoji nerdEmoji = DiscordEmoji.FromName(client, ":cokoComfyNerdge:");
            await @event.Message.CreateReactionAsync(nerdEmoji);
        }
    }
}

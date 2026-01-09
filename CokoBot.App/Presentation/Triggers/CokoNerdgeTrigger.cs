using CokoBot.App.Application.Triggers;
using CokoBot.App.Domain.Interfaces;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Presentation.Triggers
{
    public class CokoNerdgeTrigger : ITrigger
    {
        private readonly ITriggerConfig _config;

        public CokoNerdgeTrigger(ITriggerConfigFactory factory)
        {
            var triggerName = GetType().Name.Replace("Trigger", "");
            _config = factory.Create(triggerName);
        }
        public bool Matches(string message) => _config.Keywords.Any(k => message.Contains(k));
        public async Task Execute(DiscordClient client, MessageCreateEventArgs @event)
        {
            DiscordEmoji nerdEmoji = DiscordEmoji.FromName(client, ":cokoComfyNerdge:");
            await @event.Message.CreateReactionAsync(nerdEmoji);
        }
    }
}

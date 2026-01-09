using CokoBot.App.Application.Triggers;
using CokoBot.App.Domain.Interfaces;
using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Presentation.Triggers
{
    public class CokoGrokTrigger : ITrigger
    {
        private readonly ITriggerConfig _config;

        public CokoGrokTrigger(ITriggerConfigFactory factory)
        {
            var triggerName = GetType().Name.Replace("Trigger", "");
            _config = factory.Create(triggerName);
        }
        public bool Matches(string message) => _config.Keywords.Any(k => message.Contains(k));
        public async Task Execute(DiscordClient client, MessageCreateEventArgs e)
        {
            await e.Message.RespondAsync(
                "https://media.discordapp.net/attachments/922504536541757443/1440454495866851328/image.png?ex=691e374a&is=691ce5ca&hm=bef95c03fc39f73bd936227080b64d99097d62dd50f0d5ad9604471ac6b7e229&=&format=webp&quality=lossless&width=590&height=648"
            );
        }
    }
}

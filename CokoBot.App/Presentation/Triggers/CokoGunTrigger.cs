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
    public class CokoGunTrigger : ITrigger
    {
        private readonly ITriggerConfig _config;

        public CokoGunTrigger(ITriggerConfigFactory factory)
        {
            var triggerName = GetType().Name.Replace("Trigger", "");
            _config = factory.Create(triggerName);
        }
        public bool Matches(string message) => _config.Keywords.Any(k => message.StartsWith(k));
        public async Task Execute(DiscordClient client, MessageCreateEventArgs e)
        {
            await e.Channel.SendMessageAsync(
                "https://media.discordapp.net/attachments/974678061960286218/1435800190551593030/narzlGun.png?ex=690d48a1&is=690bf721&hm=c7826db6d02c02af777b9431061b65aa4204a397cdb9fba19d8b445ddd5781fb&=&format=webp&quality=lossless"
            );
        }
    }
}

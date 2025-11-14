using CokoBot.App;
using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Triggers
{
    public class CokoPanTrigger : ITrigger
    {
        private readonly string[] _keywords = Startup.AppSettings.BotSettings.Triggers.CokoPanTriggers;

        public bool Matches(string message)
        {
            return _keywords.Any(k => message.Contains(k));
        }

        public async Task Execute(DiscordClient client, MessageCreateEventArgs e)
        {
            await e.Channel.SendMessageAsync(
                "https://media.discordapp.net/attachments/974678061960286218/1435548987913404490/narlzcokopan.png?ex=690c5ead&is=690b0d2d&hm=095fbb655ba6aa5994642009b376d661a0f2e0dddf578c5e7e17fcdfa622c439&=&format=webp&quality=lossless&width=558&height=647"
            );
        }
    }
}

using CokoBot.App.Domain.Interfaces;
using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Application.Dispactcher
{
    public class TriggerDispatcher
    {
        private readonly IEnumerable<ITrigger> _triggers;

        public TriggerDispatcher(IEnumerable<ITrigger> triggers)
        {
            _triggers = triggers;
        }

        public async Task DispatchAsync(string message, DiscordClient client, MessageCreateEventArgs @event)
        {
            var trigger = _triggers.FirstOrDefault(t => t.Matches(message));
            if (trigger != null)
            {
                await trigger.Execute(client, @event);
            }
        }
    }

}

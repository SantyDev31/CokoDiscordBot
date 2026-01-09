using CokoBot.App.Application.Triggers;
using CokoBot.App.Presentation;
using CokoBot.Core.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Infrastructure.Configuration.Triggers
{
    public class TriggerConfig : ITriggerConfig
    {
        public string[] Keywords { get; }

        public TriggerConfig(IOptions<AppSettings> options, string triggerName)
        {
            Keywords = Startup.AppSettings.BotSettings.Triggers[triggerName];
        }
    }
    
}

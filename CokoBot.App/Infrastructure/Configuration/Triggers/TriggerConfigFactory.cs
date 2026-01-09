using CokoBot.App.Application.Triggers;
using CokoBot.Core.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Infrastructure.Configuration.Triggers
{
    public class TriggerConfigFactory : ITriggerConfigFactory
    {
        private readonly IOptions<AppSettings> _options;

        public TriggerConfigFactory(IOptions<AppSettings> options)
        {
            _options = options;
        }

        public ITriggerConfig Create(string triggerName)
        {
            return new TriggerConfig(_options, triggerName);
        }
    }
}

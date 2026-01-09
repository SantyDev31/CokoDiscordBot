using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Application.Triggers
{
    public interface ITriggerConfig
    {
        string[] Keywords { get; }
    }
    public interface ITriggerConfigFactory
    {
        ITriggerConfig Create(string triggerName);
    }
}

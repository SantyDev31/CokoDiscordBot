using CokoBot.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.AI.Configuration
{
    public class AIConfig
    {
        public CokoAI CokoAI { get; set; }
    }
    public class CokoAI
    {
        public string[] Personality { get; set; }
        public string[] Emotes { get; set; }
        public string[] Parameters { get; set; }
    }
}
    

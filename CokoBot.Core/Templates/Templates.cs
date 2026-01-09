using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.Core.Templates
{
    public class Templates
    {
        public static string DailyCokoMessage(string songTitle, string songURL, string songType, string userName, string userURL)
        {
            return $"# Daily Coko Recomendation \n" +
                "<@&1440781667680915457> \n" +
                $"In **Today's recomendations** we have \n" +
                $"## [{songTitle} | {songType}]({songURL}) \n" +
                $"## Made by [{userName}]({userURL})";
        }
    }
}

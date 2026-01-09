namespace CokoBot.Core.Configuration
{
    public class AppSettings
    {
        public BotSettings BotSettings { get; set; }
    }

    public class BotSettings
    {
        public string Token { get; set; }
        public string Prefix { get; set; }
        public ulong DailyCokoChannel { get; set; }
        public ulong OwnerId { get; set; }
        public Dictionary<string, string[]> Triggers { get; set; }
    }

    //public class Triggers
    //{
    //    public string[] CokoPanTriggers { get; set; }
    //    public string[] CokoGunTriggers { get; set; }
    //    public string[] CokoGrokTriggers { get; set; }
    //    public string[] CokoTiredTriggers { get; set; }

    //    public string[] CokoNerdgeTriggers { get; set; }
    //    public string[] CokoYesNoTriggers { get; set; }
    //}

}

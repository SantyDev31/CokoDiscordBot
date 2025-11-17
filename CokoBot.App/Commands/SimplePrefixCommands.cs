using CokoBot.AI;
using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Commands
{
    public class SimplePrefixCommands
    {
        [Command("test")]
        public async Task TestCommand(string msg, MessageCreateEventArgs @event)
        {
            await @event.Channel.SendMessageAsync("Test Works");
        }

        [Command("forgor")]
        public async Task ForgotCommand(string msg, MessageCreateEventArgs @event)
        {
            await @event.Channel.SendMessageAsync("マスター…テスト終わったあと、毎回設定し直すの忘れちゃダメなんだよ？");
            await DailyCoko.SendSong();
        }

        [Command("roulette")]
        public async Task RouletteCommand(string msg, MessageCreateEventArgs @event)
        {
            var Options = msg.Split('/', StringSplitOptions.RemoveEmptyEntries);
            int Selected = Program.random.Next(0, Options.Length - 1);

            await @event.Channel.SendMessageAsync($"The winning option: {Options[Selected]}");
        }
        [Command("coko")]
        public async Task CokoAICommand(string msg, MessageCreateEventArgs @event)
        {
            await @event.Channel.TriggerTypingAsync();
            string response = "";
            if (@event.Guild != null)
            {
                response = await LLMClient.SendPrompt(@event.Guild.Id, $"¿{@event.Message.Author.Username}¿:{msg}", true);
            }
            else
            {
                response = await LLMClient.SendPrompt(@event.Author.Id, $"¿{@event.Message.Author.Username}¿:{msg}", false);
            }
            await @event.Channel.SendMessageAsync($"{response}");
        }
    }
}

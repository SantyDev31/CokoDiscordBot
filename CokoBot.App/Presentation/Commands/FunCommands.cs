using CokoBot.AI;
using CokoBot.App.Application.Commands.Attributes;
using CokoBot.App.Domain.Interfaces;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Text.RegularExpressions;

namespace CokoBot.App.Presentation.Commands
{
    public class FunCommands : ICommandModule
    {
        [Command("dice")]
        public async Task DiceCommand(string msn, MessageCreateEventArgs @event)
        {
            var split = msn.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in split)
            {
                if (int.TryParse(part, out int value))
                {
                    if (value <= 0) value *= -1;
                    if (value == 1 || value == 0) value = 2;
                    await @event.Channel.SendMessageAsync($"{Program.random.Next(1, value)}");
                    return;
                }
            }
            await @event.Channel.SendMessageAsync($"Couldn't find how many sides you wanted for the dice >-<");
        }
        [Command("kiss")]
        public async Task KissCommand(string msn, MessageCreateEventArgs @event)
        {
            Regex pattern = new Regex(@"[<@>]");
            var split = msn.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            foreach (string part in split)
            {
                string partCleaned = pattern.Replace(part, "");

                await @event.Channel.SendMessageAsync(new DiscordMessageBuilder()
                    .WithContent($"Your chances to kiss {part} are {Program.random.Next(1, 100)}% kon~!")
                    .SuppressNotifications());
                return;
            }
            await @event.Channel.SendMessageAsync($"Couldn't find that user kon (｣°ﾛ°)｣");
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

using CokoBot.App.Application.Commands.Attributes;
using CokoBot.App.Application.Handlers;
using CokoBot.App.Domain.Interfaces;
using DSharpPlus.EventArgs;

namespace CokoBot.App.Presentation.Commands.MiniGames
{
    public class HangManCommands : ICommandModule
    {
        [Command("hangman")]
        public async Task HangManStartGame(string msg, MessageCreateEventArgs @event)
        {
            await @event.Message.DeleteAsync();
            string word = msg.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
            string response = HangManHandler.StartGame(@event.Guild.Id, word) ? "A game of hangman has started" : "A game of hangman is already being played";

            await @event.Channel.SendMessageAsync($"{response}");
        }

        [Command("guess")]
        public async Task HangManGuessWord(string msg, MessageCreateEventArgs @event)
        {
            string word = msg.Split(' ', StringSplitOptions.RemoveEmptyEntries)[0];
            GuessOutcome? guessOutcome = await HangManHandler.PlayGame(@event.Guild.Id, msg);
            if (guessOutcome == null)
            {
                await @event.Channel.SendMessageAsync("No game of HangMan is being played");
            }

            switch (guessOutcome?.guessState)
            {
                case GuessState.Correct:
                    await @event.Channel.SendMessageAsync(guessOutcome.wordState);
                    await @event.Channel.SendMessageAsync("That letter its on the word");
                    break;
                case GuessState.Incorrect:
                    await @event.Channel.SendMessageAsync(guessOutcome.wordState);
                    await @event.Channel.SendMessageAsync("That letter its **NOT** on the word");
                    break;
                case GuessState.Lost:
                    await @event.Channel.SendMessageAsync(guessOutcome.wordState);
                    await @event.Channel.SendMessageAsync("You Lost");
                    await @event.Channel.SendMessageAsync($"The game has ended");
                    break;
                case GuessState.Won:
                    await @event.Channel.SendMessageAsync(guessOutcome.wordState);
                    await @event.Channel.SendMessageAsync("You Win");
                    await @event.Channel.SendMessageAsync($"The game has ended");
                    break;
            }
        }
    }
}

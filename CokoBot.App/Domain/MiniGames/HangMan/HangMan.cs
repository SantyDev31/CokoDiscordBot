using CokoBot.App.Application.Handlers;
using CokoBot.App.Extension;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Domain.MiniGames.HangMan
{
    public class HangMan
    {
        private string word { get; set; }
        private string secretWord { get; set; }
        private int tries { get; set; }
        public bool isEnded { get; set; }

        public HangMan(string word)
        {
            this.word = word;
            for (int i = 0; i < word.Length; i++)
            {
                secretWord += "?";
            }
            tries = 0;
            isEnded = false;
        }

        public async Task<GuessOutcome> Guess(char letter)
        {
            if (word.Contains(letter))
            {
                foreach (int index in word.AllIndicesOf(letter.ToString()))
                {
                    secretWord = secretWord.ReplaceAt(index, letter);
                }
                if (secretWord.Contains('?'))
                {
                    return new GuessOutcome(GuessState.Correct, secretWord);
                }
                isEnded = true;
                return new GuessOutcome(GuessState.Won, secretWord);
            }
            else
            {
                tries++;
                if (tries < 8)
                {
                    return new GuessOutcome(GuessState.Incorrect, secretWord);
                }
                isEnded = true;
                return new GuessOutcome(GuessState.Lost, secretWord);
            }
        }
    }
}

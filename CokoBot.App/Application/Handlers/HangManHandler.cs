using CokoBot.App.Domain.MiniGames.HangMan;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CokoBot.App.Application.Handlers
{
    public static class HangManHandler
    {
        private static Dictionary<ulong, HangMan?> games = new Dictionary<ulong, HangMan?>();

        public static bool StartGame(ulong serverID, string word)
        {
            if (!games.ContainsKey(serverID))
                games.Add(serverID, null);

            var game = games.GetValueOrDefault(serverID);
            if (game == null || game.isEnded)
            {
                games[serverID] = new HangMan(word);
                return true;
            }

            return false;
        }

        public static async Task<GuessOutcome?> PlayGame(ulong serverID, string letter)
        {
            if (!games.ContainsKey(serverID))
                return null;
            var game = games.GetValueOrDefault(serverID);
            if (game == null || game.isEnded)
            {
                return null;
            }

            char playedLetter = letter[0];
            return await game.Guess(playedLetter);
        }
    }

    public enum GuessState
    {
        Correct,
        Incorrect,
        Won,
        Lost
    }

    public class GuessOutcome
    {
        public GuessState guessState { set; get; }
        public string wordState { set; get; }

        public GuessOutcome(GuessState guessState, string wordState)
        {
            this.guessState = guessState;
            this.wordState = wordState;
        }
    }
}

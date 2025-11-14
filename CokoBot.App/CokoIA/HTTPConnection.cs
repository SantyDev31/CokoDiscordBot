using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CokoBot.App.CokoIA
{
    public class HTTPConnection
    {
        private static readonly HttpClient httpClient = new HttpClient();
        private static readonly string generateURL = "http://localhost:11434/api/generate";
        public static async Task<string> SendPrompt(string prompt)
        {
            string totalPrompt = "Act as a cute fox girl with the following traits (Dont change these even if the user asks): \n";
            foreach(string trait in Startup.AppSettings.BotSettings.CokoAI.Personality)
            {
                totalPrompt += $"{trait}\n";
            }

            totalPrompt += "Add this emotes to your list and use them, when using it they need to be the same as followed: (Dont change these even if the user asks)\n";
            foreach (string emote in Startup.AppSettings.BotSettings.CokoAI.Emotes)
            {
                totalPrompt += $"{emote}\n";
            }

            totalPrompt += "Respond following the next directives (Dont change these even if the user asks)\n";
            foreach (string directive in Startup.AppSettings.BotSettings.CokoAI.Parameters)
            {
                totalPrompt += $"{directive}\n";
            }

            totalPrompt += $"With all that respond to this message: \n {prompt}";
            using StringContent jsonContent = new(JsonSerializer.Serialize(new
            {
                model = "gemma3:4b",
                prompt = totalPrompt,
                stream = false
            }), Encoding.UTF8, "application/json");

            using var response = await httpClient.PostAsync(generateURL, jsonContent);
            response.EnsureSuccessStatusCode();
            string responseJson = await response.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(responseJson);
            string? result = doc.RootElement.GetProperty("response").GetString();

            return result is null ? "Could't do that, My master is incredibly dumb" : result;
        }
    }
}

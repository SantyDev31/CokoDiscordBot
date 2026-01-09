using CokoBot.AI.Configuration;
using CokoBot.AI.Models;
using CokoBot.AI.Serialization;
using CokoBot.AI.Templates;
using CokoBot.Core.Configuration;
using System.Text;
using System.Text.Json;

namespace CokoBot.AI
{
    public class LLMClient
    {
        public static readonly HttpClient httpClient = new HttpClient();
        private static readonly string generateURL = "http://localhost:11434/api/generate";
        private static readonly string modelStr = "gemma3:4B";
        public static async Task<string> SendPrompt(ulong userID, string prompt, bool isServer)
        {
            int tokens = isServer ? 80 : 2000;
            string totalPrompt = SystemPromptTemplate.systemPrompt + prompt;

            int[]? pastContext = [];
            if (!isServer)
            {
                pastContext = JsonSerializerService.ReadJson(""+userID);
            }

            using StringContent jsonContent = new(JsonSerializer.Serialize(new
            {
                model = modelStr,
                prompt = totalPrompt,
                num_predict = tokens,
                context = pastContext,
                stream = false
            }), Encoding.UTF8, "application/json");

            using var response = await httpClient.PostAsync(generateURL, jsonContent);
            response.EnsureSuccessStatusCode();
            IAResponseJSON? responseJSON = JsonSerializer.Deserialize<IAResponseJSON>(await response.Content.ReadAsStringAsync());

            if (responseJSON is null)
            {
                return "Could't do that, My master is incredibly dumb";
            }
            if (!isServer)
            {
                JsonSerializerService.SaveJson(userID, responseJSON.context);
            }
            return responseJSON.response;
        }
    }
}

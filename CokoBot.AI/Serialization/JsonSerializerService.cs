using CokoBot.AI.Configuration;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CokoBot.AI.Serialization
{
    public class JsonSerializerService
    {
        private static string jsonContextPath = Path.Combine("Configuration", "context.json");
        private static string jsonAIConfigPath = Path.Combine("Configuration", "aiconfig.json");

        public static AIConfig LoadConfig()
        {
            string jsonFile = File.ReadAllText(jsonAIConfigPath);
            return JsonSerializer.Deserialize<AIConfig>(jsonFile);
        }
        public static int[] ReadJson(string userID)
        {
            string jsonFile = File.ReadAllText(jsonContextPath);

            Dictionary<string, int[]> contextJson = JsonSerializer.Deserialize<Dictionary<string, int[]>>(jsonFile);
      
            if(contextJson == null)
            {
                return [];
            }

            if (contextJson.ContainsKey(userID))
            {
                var arr = contextJson[userID];
                if (arr is null)
                {
                    return [];
                }
                int take = Math.Min(arr.Length, 5000);

                return arr[^take..];
            }

            else
            {
                return [];
            }
        }
        public static void SaveJson(ulong userID, int[] context)
        {
            Dictionary<ulong, int[]> data = new();

            if (File.Exists(jsonContextPath))
            {
                string existingJson = File.ReadAllText(jsonContextPath);

                if (!string.IsNullOrWhiteSpace(existingJson))
                {
                    try
                    {
                        data = JsonSerializer.Deserialize<Dictionary<ulong, int[]>>(existingJson)
                               ?? new Dictionary<ulong, int[]>();
                    }
                    catch
                    {
                        data = new Dictionary<ulong, int[]>();
                    }
                }
            }

            data[userID] = context;

            string newJson = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(jsonContextPath, newJson);
        }

    }
}

using CokoBot.DailySong.Models;
using Microsoft.Data.Sqlite;
using SQLitePCL;

namespace CokoBot.DailySong.Data
{
    public class CokoSongConnection
    {
        private static string table = Path.Combine("Data", "songs.db");
        private static string database = $"Data Source={table};";

        public static async Task<CokoSong> SendDailyCoko()
        {
            Batteries.Init();
            return await GetCokoSong();
        }

        public static async Task<CokoSong> GetCokoSong()
        {
            using (var connection = new SqliteConnection(database))
            {
                await connection.OpenAsync();
                string sql = "SELECT * FROM songs WHERE isRecommended = 0 ORDER BY RANDOM() LIMIT 1";

                using (var command = new SqliteCommand(sql, connection))
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        CokoSong song = new CokoSong
                        {
                            id = reader.GetInt32(0),
                            songName = reader.GetString(1),
                            songType = reader.GetString(2),
                            songURL = reader.GetString(3),
                            userName = reader.GetString(4),
                            userURL = reader.GetString(5),
                            isRecommended = reader.GetBoolean(6),
                        };

                        await MarkAsRecommended(song.id, connection);
                        await connection.CloseAsync();
                        return song;
                    }
                    else
                    {
                        await ResetRecommendedStatus(connection);
                        return await GetCokoSong();
                    }
                }
            }
        }

        private static async Task MarkAsRecommended(int songId, SqliteConnection connection)
        {
            string updateSql = "UPDATE songs SET isRecommended = 1 WHERE Id = @id";

            using (var command = new SqliteCommand(updateSql, connection))
            {
                command.Parameters.AddWithValue("@id", songId);
                await command.ExecuteNonQueryAsync();
            }
        }

        private static async Task ResetRecommendedStatus(SqliteConnection connection)
        {
            await connection.OpenAsync();
            string updateSql = "UPDATE songs SET isRecommended = 0";

            using (var command = new SqliteCommand(updateSql, connection))
            {
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}

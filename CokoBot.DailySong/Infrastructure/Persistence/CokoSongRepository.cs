using CokoBot.DailySong.Domain.Entities;
using CokoBot.DailySong.Domain.Interfaces;
using Microsoft.Data.Sqlite;

namespace CokoBot.DailySong.Infrastructure.Persistence
{
    public class CokoSongRepository : ICokoSongRepository
    {
        private readonly string _connectionString;

        public CokoSongRepository(string dbPath)
        {
            _connectionString = $"Data Source={dbPath}";
        }

        public async Task<CokoSong?> GetRandomAsync()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var sql = "SELECT * FROM songs WHERE isRecommended = 0 ORDER BY RANDOM() LIMIT 1";

            using var command = new SqliteCommand(sql, connection);
            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                return new CokoSong
                {
                    id = reader.GetInt32(0),
                    songName = reader.GetString(1),
                    songType = reader.GetString(2),
                    songURL = reader.GetString(3),
                    userName = reader.GetString(4),
                    userURL = reader.GetString(5),
                    isRecommended = reader.GetBoolean(6),
                };
            }

            return null;
        }
        public async Task MarkAsRecommended(int id)
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            string sql = "UPDATE songs SET isRecommended = 1 WHERE Id = @id";
            var command = new SqliteCommand(sql, connection);

            command.Parameters.AddWithValue("@id", id);
            await command.ExecuteNonQueryAsync();
        }

        public async Task ResetAllRecommendations()
        {
            using var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            string sql = "UPDATE songs SET isRecommended = 0";
            var command = new SqliteCommand(sql, connection);

            await command.ExecuteNonQueryAsync();
        }
    }
}

using Npgsql;
using SpaceBackend.Models;

namespace SpaceBackend.Repositories;

public class DatabaseRepository : IDatabaseRepository
{
    private readonly string _connectionString;

    public DatabaseRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") 
                           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    }

    public async Task<DatabaseInfo> GetDatabaseInfoAsync()
    {
        var dbInfo = new DatabaseInfo();

        try
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            dbInfo.IsConnected = true;
            dbInfo.DatabaseName = connection.Database;

            // Получаем версию PostgreSQL
            await using var command = new NpgsqlCommand("SELECT version();", connection);
            var version = await command.ExecuteScalarAsync();
            dbInfo.ServerVersion = version?.ToString();
        }
        catch (Exception ex)
        {
            dbInfo.IsConnected = false;
            dbInfo.ErrorMessage = ex.Message;
        }

        return dbInfo;
    }
}

using Npgsql;
using SpaceBackend.Models;

namespace SpaceBackend.Repositories
{
    public class IDRepository : IIDRepository
    {
        private readonly string _connectionString;

        public IDRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }

        public async Task<List<Player>> GetPlayers(string sessionid)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new NpgsqlCommand("", connection);
            var reader = command.ExecuteReader();
            List<Player> playerslist = new List<Player>();
            while (reader.Read()) 
            {
                playerslist.Add(new Player()
                {
                    Id = reader.GetString(0),
                    session = new Session() { id = sessionid },
                });
            }
            return playerslist;
        }
    }
}

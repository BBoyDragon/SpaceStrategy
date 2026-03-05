using Npgsql;
using SpaceBackend.Models;
using System;

namespace SpaceBackend.Repositories
{
    public class SessionRepositories : ISessionRepository
    {
        private readonly string _connectionString;

        public SessionRepositories(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")
                               ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        }
        public async Task<Player> CreatePeople(string Id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new NpgsqlCommand("", connection);
            command.Parameters.AddWithValue("Id", Id);
            var reader = await command.ExecuteReaderAsync();
            var a67 = new Player();
            await reader.ReadAsync();
            a67.Id = reader.GetString(0);
            await using var command2 = new NpgsqlCommand("", connection);
            command2.Parameters.AddWithValue("Person", Id);
            var reader2 = await command2.ExecuteReaderAsync();
            var b67 = new Session();
            await reader2.ReadAsync();
            b67.Id = reader2.GetString(0);
            a67.Session = b67;
            //a67.Session = reader.GetString(1);
            return a67;

        }

        public async Task<Session> CreateSession(string Id)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new NpgsqlCommand("", connection);
            command.Parameters.AddWithValue("SessionId", Id);
            var reader = await command.ExecuteReaderAsync();
            var a67 = new Session();
            await reader.ReadAsync();
            a67.Id = reader.GetString(0);
            //a67.Session = reader.GetString(1);
            return a67;
        }

        public Task<Session> FindSession(string Id)
        {
            throw new NotImplementedException();
        }

        public Task<Player> GetPerson(string Id)
        {
            throw new NotImplementedException();
        }

        public async Task<Player> UpdPerson(Player person)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new NpgsqlCommand("", connection);
            command.Parameters.AddWithValue("Person", person.Id);
            command.Parameters.AddWithValue("Session", person.Session);
            var reader = await command.ExecuteReaderAsync();
            var a67 = new Player();
            await reader.ReadAsync();
            a67.Id = reader.GetString(0);
            //a67.Session = reader.GetString(1);
            await using var command2 = new NpgsqlCommand("", connection);
            command.Parameters.AddWithValue("Session", person.Session);
            var reader2 = await command.ExecuteReaderAsync();
            var b67 = new Session();
            await reader2.ReadAsync();
            b67.Id = reader2.GetString(0);
            a67.Session = b67;
            return a67;
        }
        public async Task<Ship> CreateShip(Ship ship)
        {
            await using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();
            await using var command = new NpgsqlCommand("", connection);
            command.Parameters.AddWithValue("X", ship.x);
            var reader = await command.ExecuteReaderAsync();
            var a67 = new Ship();
            await reader.ReadAsync();
            a67.x = reader.GetInt32(0);
            await using var command2 = new NpgsqlCommand("", connection);
            command2.Parameters.AddWithValue("Y", ship.y);
            var reader2 = await command2.ExecuteReaderAsync();
            await reader2.ReadAsync();
            a67.y = reader2.GetInt32(0);
            await using var command3 = new NpgsqlCommand("", connection);
            command2.Parameters.AddWithValue("Z", ship.z);
            var reader3 = await command3.ExecuteReaderAsync();
            await reader3.ReadAsync();
            a67.z = reader3.GetInt32(0);

            //a67.Session = reader.GetString(1);
            return a67;

        }

    }
}

using Dapper;
using Npgsql;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using Microsoft.Extensions.Configuration;

namespace ClubInfluApp.Data.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.QueryFirstOrDefaultAsync<User>(
                "SELECT * FROM Users WHERE Email = @Email",
                new { Email = email }
            );
        }

        public async Task<int> CreateUserAsync(User user)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            return await connection.ExecuteAsync(
                "INSERT INTO Users (Email, Name, Role) VALUES (@Email, @Name, @Role)",
                user
            );
        }
    }
}

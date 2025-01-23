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

        public List<User> GetAllUsers()
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "select username, role from \"user\"";
                return connection.Query<User>(query).AsList();
            }
        }
    }
}

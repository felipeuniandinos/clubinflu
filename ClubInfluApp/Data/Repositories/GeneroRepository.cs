using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class GeneroRepository: IGeneroRepository
    {
        private readonly string dbConnectionString;

        public GeneroRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public List<Genero> ObtenerGeneros()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string query = "SELECT * FROM Genero";
                return connection.Query<Genero>(query).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}

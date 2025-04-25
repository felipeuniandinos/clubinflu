using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class PaisRepository : IPaisRepository
    {
        private readonly string dbConnectionString;

        public PaisRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public List<Pais> ObtenerPaises()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query = "SELECT * FROM Pais";
                return connection.Query<Pais>(query).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}

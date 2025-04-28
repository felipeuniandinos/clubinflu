using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class CiudadRepository : ICiudadRepository
    {
        private readonly string dbConnectionString;

        public CiudadRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public List<Ciudad> ObtenerCiudadesPorPaisYTermino(int idPais, string termino)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string query = "SELECT * FROM Ciudad WHERE idPais = @idPais AND LOWER(ciudad) LIKE @termino ORDER BY ciudad LIMIT 20";
                return connection.Query<Ciudad>(query, new { idPais, termino = $"%{termino.ToLower()}%" }).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}

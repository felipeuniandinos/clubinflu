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

        public List<Ciudad> ObtenerCiudadesPorEstadoYTermino(int idEstado, string termino)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string query = "SELECT * FROM Ciudad WHERE idEstado = @idEstado AND LOWER(ciudad) LIKE @termino ORDER BY ciudad LIMIT 20";
                return connection.Query<Ciudad>(query, new { idEstado, termino = $"%{termino.ToLower()}%" }).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}

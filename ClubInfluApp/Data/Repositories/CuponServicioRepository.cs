using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class CuponServicioRepository : ICuponServicioRepository
    {
        private readonly string dbConnectionString;

        public CuponServicioRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        // Implementación de métodos de la interfaz ICuponServicioRepository
        public List<CuponServicio> ObtenerCuponesPorIdServicio(int idServicio)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string query = "SELECT * FROM CuponServicio WHERE idServicio = @idServicio";
                return connection.Query<CuponServicio>(query, new { idServicio }).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}

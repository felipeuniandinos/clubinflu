using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class VideoPublicidadRepository : IVideoPublicidadRepository
    {
        private readonly string dbConnectionString;
        public VideoPublicidadRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }
        public List<string> ObtenerUrldeVideosPorIdCupon(int idCuponServicio)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string sql = "SELECT videoPublicidad FROM videoPublicidad WHERE idCuponServicio = @idCuponServicio";
                List<string> listaDeVideos = connection
                       .Query<string>(sql, new { idCuponServicio }).ToList();
                return listaDeVideos;
            }
            catch
            {
                throw;
            }
        }
    }
}

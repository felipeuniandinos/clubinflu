using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
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

        public List<CuponServicioViewModel> ObtenerCuponesPorOfertaServicio(int idOfertaServicio)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string sql = "SELECT * FROM obtener_cupones_servicio_por_oferta_servico(@p_id_ofeta_servicio);";
                List<CuponServicioViewModel> listaDeCuponesPorEmpresa = connection
                       .Query<CuponServicioViewModel>(sql, new
                       {
                           p_id_ofeta_servicio = idOfertaServicio
                       }).ToList();

                foreach (var cupon in listaDeCuponesPorEmpresa)
                {
                    string sql2 = "SELECT videoPublicidad FROM videoPublicidad WHERE idCuponServicio = @idCuponServicio";
                    List<string> listaDeVideos = connection
                           .Query<string>(sql2, new { idCuponServicio = cupon.idCuponServicio }).ToList();
                    cupon.videoPublicidad = listaDeVideos;
                }

                    return listaDeCuponesPorEmpresa;
            }
            catch
            {
                throw;
            }
        }
    }
}


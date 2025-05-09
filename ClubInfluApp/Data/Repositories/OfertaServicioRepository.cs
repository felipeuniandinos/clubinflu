using System.Data;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class OfertaServicioRepository : IOfertaServicioRepository
    {
        private readonly string dbConnectionString;

        public OfertaServicioRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public List<OfertaServicioViewModel> ObtenerOfertasDeServicioFiltradas(FiltroOfertasDeServicio filtroOfertasDeServicio)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                // Asignar 0 si viene null
                var idCategoria = filtroOfertasDeServicio.idCategoriaOferta ?? 0;
                var idEstado = filtroOfertasDeServicio.idEstado ?? 0;

                var sql = "SELECT * FROM obtener_ofertas_por_filtro(@p_id_categoria_oferta, @p_id_estado);";

                var listaDeOfertasYServicios = connection
                    .Query<OfertaServicioViewModel>(sql, new
                    {
                        p_id_categoria_oferta = idCategoria,
                        p_id_estado = idEstado
                    }).ToList();

                return listaDeOfertasYServicios;
            }
            catch
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

    }
}

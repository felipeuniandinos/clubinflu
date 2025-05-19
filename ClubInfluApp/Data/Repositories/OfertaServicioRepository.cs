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
        public const int ESTADO_CUPON_SERVICIO_PENDIENTE = 1;
        private readonly string dbConnectionString;

        public OfertaServicioRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public List<OfertaServicioViewModel> ObtenerOfertasDeServicioPorEmpresa(int idEmpresa)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string sql = "SELECT * FROM obtener_ofertas_servicios_por_empresa(@p_id_empresa);";
                List<OfertaServicioViewModel> listaDeOfertasYServiciosPorEmpresa = connection
                       .Query<OfertaServicioViewModel>(sql, new
                       {
                           p_id_empresa = idEmpresa
                       }).ToList();

                return listaDeOfertasYServiciosPorEmpresa;
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
        public List<OfertaServicioViewModel> ObtenerOfertasDeServicioFiltradas(FiltroOfertasDeServicio filtroOfertasDeServicio)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                int idCategoria = filtroOfertasDeServicio.idCategoriaOferta ?? 0;
                int idEstado = filtroOfertasDeServicio.idEstado ?? 0;

                string sql = "SELECT * FROM obtener_ofertas_por_filtro(@p_id_categoria_oferta, @p_id_estado);";

                List<OfertaServicioViewModel> listaDeOfertasYServicios = connection
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

        public int CrearOfertaDeServicio(OfertaServicio ofertaServicio)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            using NpgsqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string insertarOferta = @"
                    INSERT INTO OfertaServicio
                    (nombre, direccion, imagen, descripcion, fechaInicio, fechaFin, horaInicio, horaFin, cuposDisponibles, fechaCreacion, activo, idCategoriaOferta, idEmpresa)
                    VALUES
                    (@nombre, @direccion, @imagen, @descripcion, @fechaInicio, @fechaFin, @horaInicio, @horaFin, @cuposDisponibles, @fechaCreacion, @activo, @idCategoriaOferta, @idEmpresa)
                    RETURNING idOfertaServicio;
                ";

                int idOfertaCreada = connection.QuerySingle<int>(insertarOferta, ofertaServicio, transaction);

                string insertarCupon = @"
                    INSERT INTO CuponServico (codigo, fechaRedencion, idOfertaServicio, idEstadoCupon)
                    VALUES (@codigo, NULL, @idOfertaServicio, @idEstadoCupon);
                ";

                CuponServicio cupon;
                for (int i = 0; i < ofertaServicio.cuposDisponibles; i++)
                {
                    cupon = CrearNuevoCuponDeServicio(idOfertaCreada);

                    connection.Execute(insertarCupon, cupon, transaction);
                }

                transaction.Commit();
                return idOfertaCreada;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        private CuponServicio CrearNuevoCuponDeServicio(int idOfertaServicio)
        {
            CuponServicio cupon = new CuponServicio();
            cupon.codigo = Guid.NewGuid().ToString();
            cupon.idOfertaServicio = idOfertaServicio;
            cupon.idEstadoCupon = ESTADO_CUPON_SERVICIO_PENDIENTE;
            return cupon;
        }



    }
}

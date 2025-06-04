using System.Data.Common;
using System.Transactions;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class CuponServicioRepository : ICuponServicioRepository
    {
        public const int ESTADO_CUPON_REDIMIDO = 2;
        public const int ESTADO_CUPON_FINALIZADO = 5;
        private readonly string dbConnectionString;

        public CuponServicioRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public void ReservarCuponOfertaServicio(int idOfertaServicio, int idUsuarioInfluencer)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            NpgsqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string queryObtenerPrimerCuponDisponible = @"
                    SELECT idcuponservicio 
                    FROM cuponservicio 
                    WHERE idofertaservicio = @idOfertaServicio
                      AND idinfluencer IS NULL
                    ORDER BY idcuponservicio
                    LIMIT 1
                ";

                int? idCuponServicio = connection.QueryFirstOrDefault<int?>(queryObtenerPrimerCuponDisponible, new { idOfertaServicio }, transaction);

                if (idCuponServicio == null)
                {
                    throw new Exception("No hay cupones disponibles para esta oferta.");
                }

                string queryActualizarCupon = @"
                    UPDATE cuponservicio
                    SET fecharedencion = @fecharedencion,
                        idestadocupon = @idEstadoCupon,
                        idinfluencer = @idUsuarioInfluencer
                    WHERE idcuponservicio = @idCuponServicio
                ";

                connection.Execute(queryActualizarCupon, new
                {
                    fecharedencion = DateTime.Now,
                    idEstadoCupon = ESTADO_CUPON_REDIMIDO,
                    idUsuarioInfluencer,
                    idCuponServicio
                }, transaction);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("Error al reservar el cupón servicio.");
            }
        }

        public string ValidarCuponOfertaServicio(int idOfertaServicio, int idInfluencer)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string sql = "SELECT validar_reserva_oferta(@p_idOfertaServicio, @p_idInfluencer);";
                string MensajeValidacion = connection.Query<string>(sql, new { p_idOfertaServicio = idOfertaServicio, p_idInfluencer = idInfluencer }).SingleOrDefault();

                return MensajeValidacion;
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

        public OfertaServicioViewModel ObtenetCodigoNombreOfertaPorOfertaServicio(int idOfertaServicio)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query =
                    @"
                        SELECT 
                            cs.codigo,
                            os.nombre
                        FROM 
                            CuponServicio cs
                        JOIN 
                            OfertaServicio os ON cs.idOfertaServicio = os.idOfertaServicio
                        WHERE 
                            cs.idOfertaServicio = @idOfertaServicio;
                    ";
                return connection.QueryFirstOrDefault<OfertaServicioViewModel>(query, new { idOfertaServicio });
            }
            catch
            {
                throw;
            }
        }

        public List<CuponServicioViewModel> ObtenerCuponesPorOfertaServicio(int idOfertaServicio)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string sqlListaDeCuponesPorEmpresa = "SELECT * FROM obtener_cupones_servicio_por_oferta_servico(@p_id_ofeta_servicio);";
                List<CuponServicioViewModel> listaDeCuponesPorEmpresa = connection
                       .Query<CuponServicioViewModel>(sqlListaDeCuponesPorEmpresa, new
                       {
                           p_id_ofeta_servicio = idOfertaServicio
                       }).ToList();

                foreach (var cupon in listaDeCuponesPorEmpresa)
                {
                    string sqllistaDeVideosPorCupon = "SELECT videoPublicidad FROM videoPublicidad WHERE idCuponServicio = @idCuponServicio";
                    List<string> listaDeVideosPorCupon = connection
                           .Query<string>(sqllistaDeVideosPorCupon, new { idCuponServicio = cupon.idCuponServicio }).ToList();
                    cupon.videoPublicidad = listaDeVideosPorCupon;
                }

                return listaDeCuponesPorEmpresa;
            }
            catch
            {
                throw;
            }
        }

        public string validarCuponDeServicioPorCodigo(string codigoDeCuponAValidar)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string validarCuponQuery = "SELECT * FROM validar_cupon_servicio_por_codigo(@p_codigoCuponServicio);";

                string mensajeDeRespuesta = connection
                    .Query<string>(validarCuponQuery, new
                    {
                        p_codigoCuponServicio = codigoDeCuponAValidar
                    }).FirstOrDefault() ?? "Error en base de datos";

                return mensajeDeRespuesta;
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

        public List<CuponServicioViewModel> ListarCuponesServicioPorInfluencer(int idInfluencer)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string sqlObtenerCuponerPorInfluencer = "SELECT * FROM obtener_cupones_por_influencer(@p_id_influencer);;";
                List<CuponServicioViewModel> cuponesServicio = connection.Query<CuponServicioViewModel>(sqlObtenerCuponerPorInfluencer, new { p_id_influencer = idInfluencer }).ToList();

                return cuponesServicio;
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

        public CuponServicioViewModel ObtenerCuponServicioPorIdCuponServicio(int idCuponServicio)
        {
            return ObtenerInformacionCuponServicioPorIdCuponServicio(idCuponServicio);

        }

        private CuponServicioViewModel ObtenerInformacionCuponServicioPorIdCuponServicio(int idCuponServicio)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query = 
                @"
                    SELECT 
                        cs.idcuponservicio, 
                        cs.codigo,
                        cs.fechaRedencion,
                        ec.estadoCupon AS nombreEstadoCupon,
                        i.nombre AS nombreInfluencer
                    FROM 
                        CuponServicio cs
                    JOIN 
                        EstadoCupon ec ON cs.idEstadoCupon = ec.idEstadoCupon
                    LEFT JOIN 
                        Influencer i ON cs.idInfluencer = i.idInfluencer
                    WHERE 
                        cs.idCuponServicio = @idCuponServicio;
                ";

                CuponServicioViewModel respuestaObtenerCuponServicion = connection.QueryFirstOrDefault<CuponServicioViewModel>(query, new { idCuponServicio });

                if (respuestaObtenerCuponServicion == null)
                    return null;

                string queryVideos = 
                @"
                    SELECT videoCupon 
                    FROM VideoCupon 
                    WHERE idCuponServicio = @idCuponServicio;
                ";

                var videos = connection.Query<string>(queryVideos, new { idCuponServicio }).ToList();
                respuestaObtenerCuponServicion.videoCupones = videos;

                string sqlObtenerCondicionesPendientes = "SELECT validar_videos_cupon(@id_cupon_servicio);;";
                string respuestaObtenerCondicionPendiente = connection.Query<string>(sqlObtenerCondicionesPendientes, new { id_cupon_servicio = idCuponServicio }).FirstOrDefault();
                respuestaObtenerCuponServicion.condicionesPendientes = respuestaObtenerCondicionPendiente;

                return respuestaObtenerCuponServicion;
            }
            catch
            {
                throw;
            }
        }


        public CuponServicioViewModel SubirVideoCuponServicio(int idCuponServicio, VideoCupon videoCupon)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            using NpgsqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string queryIsertarVideo =
                    @"
                        INSERT INTO VideoCupon (videoCupon, fechaCreacion, idCuponServicio)
                        VALUES (@videoCupon, @fechaCreacion, @idCuponServicio);
                    ";
                connection.Execute(queryIsertarVideo, videoCupon, transaction);

                string sqlObtenerCondicionesPendientes = "SELECT validar_videos_cupon(@id_cupon_servicio);;";
                string respuestaObtenerCondicionPendiente = connection.Query<string>(sqlObtenerCondicionesPendientes, new { id_cupon_servicio = idCuponServicio }).FirstOrDefault();

                if(respuestaObtenerCondicionPendiente == "Correcto")
                {
                    string queryActualizarEstadoCupon = @"
                        UPDATE CuponServicio
                        SET idEstadoCupon = @idEstadoCupon
                        WHERE idCuponServicio = @idCuponServicio;
                    ";
                    connection.Execute(queryActualizarEstadoCupon, new { idEstadoCupon = ESTADO_CUPON_FINALIZADO, idCuponServicio }, transaction);
                }

                transaction.Commit();
                return ObtenerInformacionCuponServicioPorIdCuponServicio(idCuponServicio);
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}

     

       
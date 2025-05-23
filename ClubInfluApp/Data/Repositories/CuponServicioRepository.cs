using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class CuponServicioRepository: ICuponServicioRepository
    {
        public const int ESTADO_CUPON_REDIMIDO = 2;
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
                string MensajeValidacion = connection.Query<string>(sql, new { p_idOfertaServicio = idOfertaServicio, p_idInfluencer = idInfluencer}).SingleOrDefault();

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
    }
}

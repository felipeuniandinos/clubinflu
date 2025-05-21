using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class CuponServicioRepository: ICuponServicioRepository
    {
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
                    FROM cuponservico 
                    WHERE idofertaservicio = @idOfertaServicio
                      AND idinfluencer IS NULL
                    ORDER BY idcuponservicio
                    LIMIT 1
                ";

                int? idCuponServicio = connection.QueryFirstOrDefault<int?>(queryObtenerPrimerCuponDisponible, new { idOfertaServicio }, transaction);

                if (idCuponServicio == null)
                {
                    throw new Exception("No hay cupones disponibles para esta oferta."); //Recuerda lo de BL
                }

                string queryActualizarCupon = @"
                    UPDATE cuponservico
                    SET fecharedencion = @fecharedencion,
                        idestadocupon = @idEstadoCupon,
                        idinfluencer = @idUsuarioInfluencer
                    WHERE idcuponservicio = @idCuponServicio
                ";

                connection.Execute(queryActualizarCupon, new
                {
                    fecharedencion = DateTime.Now,
                    idEstadoCupon = 2, // Dejarlo como constante REDIMIDO
                    idUsuarioInfluencer,
                    idCuponServicio
                }, transaction);

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw new Exception("Error al reservar el cupón servicio."); //Recuerda lo de BL
            }
        }
    }
}

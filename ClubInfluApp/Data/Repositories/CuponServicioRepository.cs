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

        public void ReservarCuponOfertaServicio(CuponServicioViewModel cuponServicioViewModel)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                string queryReservarCuponOfertaServicio =
                    @" UPDATE cuponservico
                        SET fecharedencion = @fecharedencion,
                            idestadocupon = @idNuevoEstadoCupon,
                            idinfluencer = @idUsuarioInfluencer
                        WHERE idcuponservicio  = @idcuponservicio
                    ";

                connection.Execute(
                    queryReservarCuponOfertaServicio,new{ cuponServicioViewModel }, transaction );

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al reservar el cupon servicio.", ex);
                ;
            }
        }
    }
}

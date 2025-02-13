using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class UsuarioEmpresaRepository : IUsuarioEmpresaRepository
    {
        private readonly string dbConnectionString;

        public UsuarioEmpresaRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public int CrearUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa, Empresa empresa)
        {
            using var connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                string insertarEmpresa = 
                    @"
                        INSERT INTO Empresa 
                        (idCiudad, nombre, url, numeroContacto, sector, direccion) 
                        VALUES 
                        (@idCiudad, @nombre, @url, @numeroContacto, @sector, @direccion) 
                        RETURNING idEmpresa;
                    ";

                int idEmpresaCreada = connection.QuerySingle<int>(insertarEmpresa, empresa, transaction);
                usuarioEmpresa.idEmpresa = idEmpresaCreada;


                string insertarUsuarioEmpresa =
                    @"
                        INSERT INTO UsuarioEmpresa 
                        (idEmpresa, idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion)
                        VALUES 
                        (@idEmpresa, @idEstadoUsuario, @correo, @clave, @fechaCreacion, @fechaActualizacion)
                        RETURNING idUsuarioEmpresa;
                    "; 

                int idUsuarioEmpresa = connection.QuerySingle<int>(insertarUsuarioEmpresa, usuarioEmpresa, transaction);
                transaction.Commit(); 
                return idUsuarioEmpresa;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}

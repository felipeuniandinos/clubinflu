using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
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
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            using NpgsqlTransaction transaction = connection.BeginTransaction();

            try
            {
                if (empresa.idEmpresa == 0)
                {
                    string insertarEmpresa =
                    @"
                        INSERT INTO Empresa 
                        (idCiudad, idCiudad2, idCiudad3, idCiudad4, nombre, nif, url, numeroContacto, sector, direccion) 
                        VALUES 
                        (@idCiudad, @idCiudad2, @idCiudad3, @idCiudad4, @nombre, @nif, @url, @numeroContacto, @sector, @direccion) 
                        RETURNING idEmpresa;
                    ";

                    int idEmpresaCreada = connection.QuerySingle<int>(insertarEmpresa, empresa, transaction);
                    usuarioEmpresa.idEmpresa = idEmpresaCreada;
                }
                else
                {
                    string actualizarEmpresa =
                    @"
                        UPDATE Empresa
                        SET 
                            idCiudad = COALESCE(@idCiudad, idCiudad),
                            idCiudad2 = COALESCE(@idCiudad2, idCiudad2),
                            idCiudad3 = COALESCE(@idCiudad3, idCiudad3),
                            idCiudad4 = COALESCE(@idCiudad4, idCiudad4),
                            nombre = COALESCE(@nombre, nombre),
                            nif = COALESCE(@nif, nif),
                            url = COALESCE(@url, url),
                            numeroContacto = COALESCE(@numeroContacto, numeroContacto),
                            sector = COALESCE(@sector, sector),
                            direccion = COALESCE(@direccion, direccion)
                        WHERE idEmpresa = @idEmpresa;
                    ";

                    connection.Execute(actualizarEmpresa, empresa, transaction);
                }


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

        public UsuarioEmpresa ObtenerUsuarioEmpresaValidoPorCorreoYEmpresa(string correo, int idEmpresa)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query = "SELECT * FROM UsuarioEmpresa WHERE correo = @correo and idEmpresa = @idEmpresa";
                return connection.QueryFirstOrDefault<UsuarioEmpresa>(query, new { correo, idEmpresa });
            }
            catch
            {
                throw;
            }

        }

        public Empresa ObtenerEmpresaPorNif(string nif)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query = "SELECT * FROM Empresa WHERE nif = @nif";
                return connection.QueryFirstOrDefault<Empresa>(query, new { nif });
            }
            catch
            {
                throw;
            }
        }

        public List<UsuarioEmpresaViewModel> ObtenerUsuariosEmpresa()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {

                string informacionListaUsuarioEmpresa =
                    @"  SELECT ue.idUsuarioEmpresa, ue.correo, eu.estadoUsuario, ue.fechaCreacion
                        FROM UsuarioEmpresa ue
                        JOIN EstadoUsuario eu ON ue.idEstadoUsuario = eu.idEstadoUsuario;
                    ";

                List<UsuarioEmpresaViewModel> listaListaUsuarioEmpresa = connection.Query<UsuarioEmpresaViewModel>(informacionListaUsuarioEmpresa).ToList();

                return listaListaUsuarioEmpresa;

            }
            catch
            {
                throw;
            }
        }
    }
}

using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Helpers;
using ClubInfluApp.Models;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly string dbConnectionString;

        public UsuarioRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public int AutenticarUsuarioAdministrador(string correo, string clave)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query = "SELECT * FROM UsuarioAdministrador WHERE correo = @correo AND idEstadoUsuario = 2";
                UsuarioAdministrador usuario = connection.QueryFirstOrDefault<UsuarioAdministrador>(query, new { correo });

                if (usuario == default)
                {
                    return 0;
                }
                bool esClaveValida = HashHelper.VerificarHash(clave, usuario.clave);
                return esClaveValida ? usuario.idUsuarioAdministrador : 0;
            }
            catch
            {
                throw;
            }
        }

        public int AutenticarUsuarioEmpresa(string correo, string clave)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query = "SELECT * FROM UsuarioEmpresa WHERE correo = @correo AND idEstadoUsuario = 2";
                UsuarioEmpresa usuario = connection.QueryFirstOrDefault<UsuarioEmpresa>(query, new { correo });

                if (usuario == default)
                {
                    return 0;
                }
                bool esClaveValida = HashHelper.VerificarHash(clave, usuario.clave);
                return esClaveValida ? usuario.idUsuarioEmpresa : 0;
            }
            catch
            {
                throw;
            }
        }

        public int AutenticarUsuarioInfluencer(string correo, string clave)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query = "SELECT * FROM UsuarioInfluencer WHERE correo = @correo AND idEstadoUsuario = 2";
                UsuarioInfluencer usuario = connection.QueryFirstOrDefault<UsuarioInfluencer>(query, new { correo });

                if (usuario == default)
                {
                    return 0;
                }
                bool esClaveValida = HashHelper.VerificarHash(clave, usuario.clave);
                return esClaveValida ? usuario.idUsuarioInfluencer : 0;
            }
            catch
            {
                throw;
            }
        }
    }
}

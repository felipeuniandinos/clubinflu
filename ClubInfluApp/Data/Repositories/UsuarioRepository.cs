using ClubInfluApp.Data.Interfaces;
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
                string query = "SELECT idUsuarioAdministrador FROM UsuarioAdministrador WHERE correo = @correo AND clave = @clave AND idEstadoUsuario = 2";
                return connection.QueryFirstOrDefault<int>(query, new { correo, clave });
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
                string query = "SELECT idUsuarioEmpresa FROM UsuarioEmpresa WHERE correo = @correo AND clave = @clave AND idEstadoUsuario = 2";
                return connection.QueryFirstOrDefault<int>(query, new { correo, clave });
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
                string query = "SELECT idUsuarioInfluencer FROM UsuarioInfluencer WHERE correo = @correo AND clave = @clave AND idEstadoUsuario = 2";
                return connection.QueryFirstOrDefault<int>(query, new { correo, clave });
            }
            catch
            {
                throw;
            }
        }
    }
}

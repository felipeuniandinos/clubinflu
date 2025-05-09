using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class EstadoRepository : IEstadoRepository
    {
        private readonly string dbConnectionString;

        public EstadoRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public Estado ObtenerEstadoPrincipalPorIdUsuarioInfluencer(int idUsuarioInfluencer)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string query = "SELECT * FROM Estado WHERE idEstado = \r\n(SELECT c.idestado FROM Influencer i JOIN ciudad c ON i.idciudad = c.idciudad WHERE i.idinfluencer  = @idUsuarioInfluencer)";
                return connection.QueryFirstOrDefault<Estado>(query, new {idUsuarioInfluencer});
            }
            catch
            {
                throw;
            }
            
        }

        public List<Estado> ObtenerEstadosPorPaisYTermino(int idPais, string termino)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string query = "SELECT * FROM Estado WHERE idPais = @idPais AND LOWER(estado) LIKE @termino ORDER BY estado LIMIT 20";
                return connection.Query<Estado>(query, new { idPais, termino = $"%{termino.ToLower()}%" }).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}

using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly string dbConnectionString;

        public CategoriaRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public List<CategoriaOferta> ObtenerCategorias()
        {
          using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string query = "SELECT * FROM CategoriaOferta";
                return connection.Query<CategoriaOferta>(query).ToList();
            }
            catch
            {
                throw;
            }
        }      
    }
}

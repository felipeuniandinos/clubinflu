using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class RedSocialRepository : IRedSocialRepository
    {
        private readonly string dbConnectionString;

        public RedSocialRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public List<RedSocial> ObtenerRedesSociales()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string query = "SELECT * FROM RedSocial";
                return connection.Query<RedSocial>(query).ToList();
            }
            catch
            {
                throw;
            }
        }
    }
}

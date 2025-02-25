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
        public UsuarioEmpresa CrearUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa, Empresa empresa, int idCiudad)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);

            Empresa empresaCreadaEnLaDb = connection.QueryFirstOrDefault<Empresa>("INSERT INTO empresa (id_ciudad, nombre, @url, @numero_contacto, sector, direccion) VALUES (@idCiudad, @nombre, @url, @numeroContacto, @sector, @direccion) RETURNIN")

            throw new NotImplementedException();
        }
    }
}

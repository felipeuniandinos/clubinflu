using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Dapper;
using Npgsql;

namespace ClubInfluApp.Data.Repositories
{
    public class UsuarioInfluencerRepository : IUsuarioInfluencerRepository
    {
        private readonly string dbConnectionString;

        public UsuarioInfluencerRepository(IConfiguration configuration)
        {
            dbConnectionString = configuration.GetConnectionString("PostgresConnection");
        }

        public int CrearUsuarioInfluencer(UsuarioInfluencer usuarioInfluencer, Influencer influencer, List<InfluencerRedSocial> influencerRedesSociales)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            using NpgsqlTransaction transaction = connection.BeginTransaction();

            try
            {
                string insertarInfluencer =
                @"
                        INSERT INTO Influencer 
                        (idCiudad, idCiudad2, idCiudad3, idCiudad4, idGenero, nombre, fechaNacimiento, numeroContacto) 
                        VALUES 
                        (@idCiudad, @idCiudad2, @idCiudad3, @idCiudad4, @idGenero, @nombre, @fechaNacimiento, @numeroContacto) 
                        RETURNING idInfluencer;
                    ";

                int idInfluencerCreado = connection.QuerySingle<int>(insertarInfluencer, influencer, transaction);
                usuarioInfluencer.idInfluencer = idInfluencerCreado;

                foreach (InfluencerRedSocial redSocial in influencerRedesSociales)
                {
                    redSocial.idInfluencer = idInfluencerCreado;
                    string insertarInfluencerRedSocial =
                    @"
                            INSERT INTO InfluencerRedSocial 
                            (idInfluencer, idRedSocial, numeroSeguidores, fechaCreacion, fechaActualizacion)
                            VALUES 
                            (@idInfluencer, @idRedSocial, @numeroSeguidores, @fechaCreacion, @fechaActualizacion);
                        ";
                    connection.Execute(insertarInfluencerRedSocial, redSocial, transaction);
                }


                string insertarUsuarioInfluencer =
                    @"
                        INSERT INTO UsuarioInfluencer 
                        (idInfluencer, idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion)
                        VALUES 
                        (@idInfluencer, @idEstadoUsuario, @correo, @clave, @fechaCreacion, @fechaActualizacion)
                        RETURNING idInfluencer;
                    ";

                int idUsuarioEmpresa = connection.QuerySingle<int>(insertarUsuarioInfluencer, usuarioInfluencer, transaction);
                transaction.Commit();
                return idUsuarioEmpresa;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public UsuarioInfluencer ObtenerUsuarioInfluencerValidoPorCorreo(string correo)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query = "SELECT * FROM UsuarioInfluencer WHERE correo = @correo";
                return connection.QueryFirstOrDefault<UsuarioInfluencer>(query, new { correo });
            }
            catch
            {
                throw;
            }

        }

        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string InformacionInfluencer =
                @"
                    SELECT ui.idUsuarioInfluencer, ui.correo, eu.estadoUsuario, ui.fechaCreacion
                    FROM UsuarioInfluencer ui
                    JOIN EstadoUsuario eu ON ui.idEstadoUsuario = eu.idEstadoUsuario;
                ";
                List<UsuarioInfluencerViewModel> listaInfluencers = connection.Query<UsuarioInfluencerViewModel>(InformacionInfluencer).ToList();
                return listaInfluencers;
            }
            catch
            {
                throw;
            }
        }
    }
}

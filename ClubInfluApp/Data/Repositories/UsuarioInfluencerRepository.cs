using ClubInfluApp.Data.Interfaces;
//using ClubInfluApp.Models;
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
        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString); 
            connection.Open(); // Abre la conexion a la BD
           
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
            catch {
                throw;
            }
        }

        public GestionarUsuarioInfluencerViewModel GestionarUsuarioInfluencer(int idUsuarioInfluencer)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open(); 
             try
            {
                string InformacionGestionarInfluencer =
                @"
                    SELECT t1.correo, t2.estadousuario, t3.nombre, t3.numerocontacto, t3.fechanacimineto, t4.genero, t5.ciudad
                    FROM UsuarioInfluencer t1
                    JOIN EstadoUsuario t2 ON t1.idEstadoUsuario = t2.idEstadoUsuario
                    JOIN influencer t3 ON t1.idinfluencer  = t3.idinfluencer
                    JOIN genero t4 ON t3.idgenero  = t4.idgenero
                    JOIN ciudad t5 ON t3.idciudad  = t5.idciudad
                    WHERE ui.idUsuarioInfluencer = @idUsuarioInfluencer;
                ";
                GestionarUsuarioInfluencerViewModel? GestionarUsuarioInfluencer = connection.Query<GestionarUsuarioInfluencerViewModel>(InformacionGestionarInfluencer).FirstOrDefault();
                
                if (GestionarUsuarioInfluencer == null)
                {
                    throw new Exception("No se encontró el usuario influencer.");
                }

                return GestionarUsuarioInfluencer;
            }
            catch {
                throw;
            }
        }
    }
}

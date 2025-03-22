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
              
            string queryUsuarioInfluencer = 
            @"
                SELECT
                    ui.idUsuarioInfluencer,
                    ui.correo,
                    ui.fechaCreacion AS usuarioFechaCreacion,
                    ui.fechaActualizacion AS usuarioFechaActualizacion,
                    eu.estadoUsuario,
                    i.nombre,
                    i.fechaNacimineto,
                    i.numeroContacto,
                    c1.ciudad AS ciudadPrincipal,
                    p1.pais AS paisCiudadPrincipal,
                    c2.ciudad AS ciudadSecundaria,
                    p2.pais AS paisCiudadSecundaria,
                    c3.ciudad AS ciudadTerciaria,
                    p3.pais AS paisCiudadTerciaria,
                    c4.ciudad AS ciudadCuaternaria,
                    p4.pais AS paisCiudadCuaternaria,
                    g.genero
                FROM UsuarioInfluencer ui
                JOIN Influencer i ON ui.idInfluencer = i.idInfluencer
                JOIN EstadoUsuario eu ON ui.idEstadoUsuario = eu.idEstadoUsuario
                JOIN Genero g ON i.idGenero = g.idGenero
                LEFT JOIN Ciudad c1 ON i.idCiudad = c1.idCiudad
                LEFT JOIN Pais p1 ON c1.idPais = p1.idPais
                LEFT JOIN Ciudad c2 ON i.idCiudad2 = c2.idCiudad
                LEFT JOIN Pais p2 ON c2.idPais = p2.idPais
                LEFT JOIN Ciudad c3 ON i.idCiudad3 = c3.idCiudad
                LEFT JOIN Pais p3 ON c3.idPais = p3.idPais
                LEFT JOIN Ciudad c4 ON i.idCiudad4 = c4.idCiudad
                LEFT JOIN Pais p4 ON c4.idPais = p4.idPais
                WHERE ui.idUsuarioInfluencer = @idUsuarioInfluencer;";

                var gestionarUsuarioInfluencer = connection.Query<GestionarUsuarioInfluencerViewModel>(queryUsuarioInfluencer, new { idUsuarioInfluencer }).FirstOrDefault();
                if (gestionarUsuarioInfluencer == null)
                {
                    throw new Exception("No se encontró detalles de usuario influencer.");
                }

            string queryRedesSociales = 
            @"
                SELECT
                    rs.redSocial,
                    irs.numeroSeguidores,
                    irs.fechaCreacion AS redFechaCreacion,
                    irs.fechaActualizacion AS redFechaActualizacion
                FROM UsuarioInfluencer ui
                JOIN Influencer i ON ui.idInfluencer = i.idInfluencer
                JOIN InfluencerRedSocial irs ON i.idInfluencer = irs.idInfluencer
                JOIN RedSocial rs ON irs.idRedSocial = rs.idRedSocial
                WHERE ui.idUsuarioInfluencer = @idUsuarioInfluencer;";

                gestionarUsuarioInfluencer.RedesSociales = connection.Query<RedSocialViewModel>(queryRedesSociales, new { idUsuarioInfluencer }).ToList();

            string queryEstadosUsuario = 
            @"
                SELECT ue.idEstadoUsuario, ue.estadousuario FROM EstadoUsuario ue;";

                gestionarUsuarioInfluencer.EstadoUsuarios = connection.Query<EstadoUsuarioViewModel>(queryEstadosUsuario).ToList();

                return gestionarUsuarioInfluencer;
            }
            catch
            {
                throw;
            }
        }

        public void ActualizarEstadoUsuarioInfluencer(int idUsuarioInfluencer, int idEstadoUsuarioInfluencer)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();
            try
            {
                string InformacionActualizarEstadoInfluencer =
                @"
                    UPDATE usuarioinfluencer 
                    SET 
	                idestadousuario  = @idEstadoUsuarioInfluencer	
                    WHERE idusuarioinfluencer  = @idUsuarioInfluencer;
                ";
                connection.Execute(InformacionActualizarEstadoInfluencer, new { idEstadoUsuarioInfluencer, idUsuarioInfluencer }, transaction);
                transaction.Commit();            
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

    }
}

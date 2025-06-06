﻿using ClubInfluApp.Data.Interfaces;
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
                        (idInfluencer, idRedSocial, numeroSeguidores, fechaCreacion, fechaActualizacion, videoEstadisticas)
                        VALUES 
                        (@idInfluencer, @idRedSocial, @numeroSeguidores, @fechaCreacion, @fechaActualizacion, @videoEstadisticas);
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
                         i.fechaNacimiento,
                         i.numeroContacto,
                         c1.ciudad AS ciudadPrincipal,
                         e1.estado as estadoCiudadPrincipal,
                         p1.pais AS paisEstadoCiudadPrincipal,
                         c2.ciudad AS ciudadSecundaria,
                         e2.estado as estadoCiudadSecundario,
                         p2.pais AS paisEstadoCiudadSecundaria,
                         c3.ciudad AS ciudadTerciaria,
                         e3.estado as estadoCiudadTerciaria,
                         p3.pais AS paisEstadoCiudadTerciaria,
                         c4.ciudad AS ciudadCuaternaria,
                         e3.estado as estadoCiudadCuaternaria,
                         p4.pais AS paisEstadoCiudadCuaternaria,
                         g.genero
                     FROM UsuarioInfluencer ui
                     JOIN Influencer i ON ui.idInfluencer = i.idInfluencer
                     JOIN EstadoUsuario eu ON ui.idEstadoUsuario = eu.idEstadoUsuario
                     JOIN Genero g ON i.idGenero = g.idGenero
                     LEFT JOIN Ciudad c1 ON i.idCiudad = c1.idCiudad
                     LEFT JOIN Estado e1 ON c1.idEstado = e1.idestado 
                     LEFT JOIN Pais p1 ON e1.idPais = p1.idPais
                     LEFT JOIN Ciudad c2 ON i.idCiudad2 = c2.idCiudad
                     LEFT JOIN Estado e2 ON c2.idEstado = e2.idestado 
                     LEFT JOIN Pais p2 ON e2.idPais = p2.idPais
                     LEFT JOIN Ciudad c3 ON i.idCiudad3 = c3.idCiudad
                     LEFT JOIN Estado e3 ON c3.idEstado = e3.idestado 
                     LEFT JOIN Pais p3 ON e3.idPais = p3.idPais
                     LEFT JOIN Ciudad c4 ON i.idCiudad4 = c4.idCiudad
                     LEFT JOIN Estado e4 ON c4.idEstado = e4.idestado 
                     LEFT JOIN Pais p4 ON e4.idPais = p4.idPais
                    WHERE ui.idUsuarioInfluencer = @idUsuarioInfluencer;
                ";

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
                        irs.fechaActualizacion AS redFechaActualizacion,
                        irs.videoestadisticas as videoEstadisticas
                    FROM UsuarioInfluencer ui
                    JOIN Influencer i ON ui.idInfluencer = i.idInfluencer
                    JOIN InfluencerRedSocial irs ON i.idInfluencer = irs.idInfluencer
                    JOIN RedSocial rs ON irs.idRedSocial = rs.idRedSocial
                    WHERE ui.idUsuarioInfluencer = @idUsuarioInfluencer;
                ";

                gestionarUsuarioInfluencer.RedesSociales = connection.Query<RedSocialViewModel>(queryRedesSociales, new { idUsuarioInfluencer }).ToList();

                string queryEstadosUsuario = 
                @"
                    SELECT ue.idEstadoUsuario, ue.estadousuario FROM EstadoUsuario ue;
                ";
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
        
        public int ObtenerEstadoUsuarioInfluencer(int idUsuarioInfluencer)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string queryObtenerEstadoUsuarioInfluencer =
                @"
                    SELECT idEstadoUsuario 
                    FROM usuarioinfluencer  
                    WHERE idusuarioinfluencer = @idUsuarioInfluencer; 
                
                ";
                return connection.QueryFirstOrDefault<int>(queryObtenerEstadoUsuarioInfluencer, new { idUsuarioInfluencer });
            }
            catch
            {
                throw;
            }
        }

        public Influencer ObtenerInfluencerPorIdUsuarioInfluencer(int idUsuarioInfluencer)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string queryObtenerInfluencerPorIdUsuarioInfluencer =
                  @"
                     SELECT e.* 
                     FROM usuarioInfluencer ue
                     JOIN Influencer e ON ue.idInfluencer = e.idInfluencer
                     WHERE ue.idUsuarioInfluencer = @idUsuarioInfluencer;
                  ";

                return connection.QuerySingle<Influencer>(queryObtenerInfluencerPorIdUsuarioInfluencer, new { idUsuarioInfluencer });
            }
            catch
            {
                throw;
            }
        }
    }
}

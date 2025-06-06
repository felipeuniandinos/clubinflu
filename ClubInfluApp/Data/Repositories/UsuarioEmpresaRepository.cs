﻿using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
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

        public int CrearUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa, Empresa empresa, TarjetaPago tarjetaPago)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            using NpgsqlTransaction transaction = connection.BeginTransaction();

            try
            {
                if (empresa.idEmpresa == 0)
                {
                    string insertarEmpresa =
                    @"
                        INSERT INTO Empresa 
                        (idCiudad, nombre, nif, url, numeroContacto, sector, direccion) 
                        VALUES 
                        (@idCiudad, @nombre, @nif, @url, @numeroContacto, @sector, @direccion) 
                        RETURNING idEmpresa;
                    ";

                    int idEmpresaCreada = connection.QuerySingle<int>(
                        insertarEmpresa,
                        empresa,
                        transaction
                    );
                    usuarioEmpresa.idEmpresa = idEmpresaCreada;
                    tarjetaPago.idEmpresa = idEmpresaCreada;
                }
                else
                {
                    string actualizarEmpresa =
                    @"
                        UPDATE Empresa
                        SET 
                            idCiudad = COALESCE(@idCiudad, idCiudad),
                            nombre = COALESCE(@nombre, nombre),
                            nif = COALESCE(@nif, nif),
                            url = COALESCE(@url, url),
                            numeroContacto = COALESCE(@numeroContacto, numeroContacto),
                            sector = COALESCE(@sector, sector),
                            direccion = COALESCE(@direccion, direccion)
                        WHERE idEmpresa = @idEmpresa;
                    ";

                    connection.Execute(actualizarEmpresa, empresa, transaction);
                }

                string insertarTarjetaPagoEmpresa =
                    @"
                        INSERT INTO TarjetaPago 
                        (idEmpresa, numeroTarjeta, nombreTitular, fechaExpiracion, codigoSeguridad, activo)
                        VALUES 
                        (@idEmpresa, @numeroTarjeta, @nombreTitular, @fechaExpiracion, @codigoSeguridad, @activo)
                        RETURNING idTarjetaPago;
                    ";

                int idTarjetaPago = connection.QuerySingle<int>(insertarTarjetaPagoEmpresa, tarjetaPago, transaction);

                string insertarUsuarioEmpresa =
                    @"
                        INSERT INTO UsuarioEmpresa 
                        (idEmpresa, idEstadoUsuario, correo, clave, fechaCreacion, fechaActualizacion)
                        VALUES 
                        (@idEmpresa, @idEstadoUsuario, @correo, @clave, @fechaCreacion, @fechaActualizacion)
                        RETURNING idUsuarioEmpresa;
                    ";

                int idUsuarioEmpresa = connection.QuerySingle<int>(insertarUsuarioEmpresa, usuarioEmpresa, transaction);
                transaction.Commit();
                return idUsuarioEmpresa;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public UsuarioEmpresa ObtenerUsuarioEmpresaValidoPorCorreoYEmpresa(string correo, int idEmpresa)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query =
                    "SELECT * FROM UsuarioEmpresa WHERE correo = @correo and idEmpresa = @idEmpresa";
                return connection.QueryFirstOrDefault<UsuarioEmpresa>(query, new { correo, idEmpresa });
            }
            catch
            {
                throw;
            }
        }

        public Empresa ObtenerEmpresaPorNif(string nif)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string query = "SELECT * FROM Empresa WHERE nif = @nif";
                return connection.QueryFirstOrDefault<Empresa>(query, new { nif });
            }
            catch
            {
                throw;
            }
        }

        public List<UsuarioEmpresaViewModel> ObtenerUsuariosEmpresa()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string informacionListaUsuarioEmpresa =
                    @"  SELECT ue.idUsuarioEmpresa, ue.correo, eu.estadoUsuario, ue.fechaCreacion
                        FROM UsuarioEmpresa ue
                        JOIN EstadoUsuario eu ON ue.idEstadoUsuario = eu.idEstadoUsuario;
                    ";

                List<UsuarioEmpresaViewModel> listaListaUsuarioEmpresa = connection
                    .Query<UsuarioEmpresaViewModel>(informacionListaUsuarioEmpresa)
                    .ToList();
                return listaListaUsuarioEmpresa;
            }
            catch
            {
                throw;
            }
        }

        public GestionarUsuarioEmpresaViewModel GestionarUsuarioEmpresa(int idUsuarioEmpresa)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();
            try
            {
                string queryDetalleUsuarioEmpresa =
                @"  SELECT 
                        ue.idUsuarioEmpresa, 
                        ue.correo, eu.estadoUsuario,
                        ue.fechaCreacion,
                        e.nombre,
                        e.nif,
                        e.url,
                        e.numeroContacto,
                        e.sector,
                        e.direccion,
                        t.numeroTarjeta,
                        t.nombreTitular,
                        t.fechaExpiracion,
                        t.codigoSeguridad,
                        c1.ciudad AS ciudad,
                        e1.estado as estado,
                        p1.pais AS pais
                    FROM UsuarioEmpresa ue
                    JOIN EstadoUsuario eu ON ue.idEstadoUsuario = eu.idEstadoUsuario
                    JOIN Empresa e ON ue.idEmpresa = e.idEmpresa
                    JOIN TarjetaPago t ON ue.idEmpresa = t.idEmpresa    
                    LEFT JOIN Ciudad c1 ON e.idCiudad = c1.idCiudad
                    LEFT JOIN Estado e1 ON c1.idEstado = e1.idestado 
                    LEFT JOIN Pais p1 ON e1.idPais = p1.idPais
                    WHERE ue.idUsuarioEmpresa = @idUsuarioEmpresa;
                ";

                GestionarUsuarioEmpresaViewModel detalleUsuarioEmpresa =
                    connection.QueryFirstOrDefault<GestionarUsuarioEmpresaViewModel>(
                    queryDetalleUsuarioEmpresa,
                     new { idUsuarioEmpresa }
                    );
                string queryEstadosUsuario =
                    @"  SELECT ue.idEstadoUsuario, ue.estadousuario FROM EstadoUsuario ue;";

                detalleUsuarioEmpresa.estadosUsuarios = connection
                    .Query<EstadoUsuario>(queryEstadosUsuario)
                    .ToList();
                return detalleUsuarioEmpresa;
            }
            catch
            {
                throw;
            }
        }

        public void ActualizarEstadoUsuarioEmpresa(int idUsuarioEmpresa, int idNuevoEstadoUsuario)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            using var transaction = connection.BeginTransaction();

            try
            {
                string queryActualizacionEstadoUsuarioEmpresa =
                    @"  UPDATE UsuarioEmpresa 
                        SET idEstadoUsuario = @idNuevoEstadoUsuario
                        WHERE idUsuarioEmpresa = @idUsuarioEmpresa
                    ";

                connection.Execute(
                    queryActualizacionEstadoUsuarioEmpresa,
                    new
                    {
                        idUsuarioEmpresa = idUsuarioEmpresa,
                        idNuevoEstadoUsuario = idNuevoEstadoUsuario,
                    },
                    transaction
                );

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw new Exception("Error al modificar el estado del usuario empresa.", ex);
                ;
            }
        }

        public int ObtenerEstadoUsuarioEmpresa(int idUsuarioEmpresa)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string queryObtenerEstadoUsuarioEmpresa =
                  @"
                    SELECT idEstadoUsuario 
                    FROM usuarioempresa   
                    WHERE idusuarioempresa  = @idUsuarioEmpresa; 
                  ";
                return connection.QueryFirstOrDefault<int>(queryObtenerEstadoUsuarioEmpresa, new { idUsuarioEmpresa });
            }
            catch
            {
                throw;
            }
        }

        public Empresa ObtenerEmpresaPorIdUsuarioEmpresa(int idUsuarioEmpresa)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(dbConnectionString);
            connection.Open();

            try
            {
                string queryObtenerEmpresaPorIdUsuarioEmpresa =
                  @"
                    SELECT e.* 
                    FROM UsuarioEmpresa ue
                    JOIN Empresa e ON ue.idEmpresa = e.idEmpresa
                    WHERE ue.idUsuarioEmpresa = @idUsuarioEmpresa;
                  ";

                return connection.QuerySingle<Empresa>(queryObtenerEmpresaPorIdUsuarioEmpresa, new { idUsuarioEmpresa });
            }
            catch
            {
                throw;
            }
        }
    }
}

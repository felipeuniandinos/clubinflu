using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Helpers;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class UsuarioEmpresaService : IUsuarioEmpresaService
    {
        public const int ESTADO_USUARIO_PENDIENTE = 1;
        private readonly IUsuarioEmpresaRepository _usuarioEmpresaRepository;

        public UsuarioEmpresaService(IUsuarioEmpresaRepository usuarioEmpresaRepository)
        {
            _usuarioEmpresaRepository = usuarioEmpresaRepository;
        }

        public int CrearUsuarioEmpresa(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel)
        {
            Empresa empresa = ObtenerEmpresaParaNuevoUsuario(nuevoUsuarioEmpresaViewModel);
            UsuarioEmpresa usuario = CrearNuevoUsuario(nuevoUsuarioEmpresaViewModel, empresa.idEmpresa);
            TarjetaPago tarjetaPago = CrearNuevaTarjetaPago(nuevoUsuarioEmpresaViewModel, empresa.idEmpresa);

            if (ExisteUnUsuarioConEseCorreoYEmpresa(usuario.correo, empresa.idEmpresa))
            {
                throw new Exception("|BL|:Ya existe un usuario asociado a la empresa con ese correo registrado en el sistema");
            }

            usuario.clave = HashHelper.GenerarHash(usuario.clave);
            return _usuarioEmpresaRepository.CrearUsuarioEmpresa(usuario, empresa, tarjetaPago);
        }

        private Empresa ObtenerEmpresaParaNuevoUsuario(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel)
        {
            Empresa empresa = CrearNuevaEmpresa(nuevoUsuarioEmpresaViewModel);
            Empresa empresaEnSistema = _usuarioEmpresaRepository.ObtenerEmpresaPorNif(nuevoUsuarioEmpresaViewModel.nif);

            if (empresaEnSistema != null)
            {
                empresa.idEmpresa = empresaEnSistema.idEmpresa;
            }
            return empresa;
        }

        private TarjetaPago CrearNuevaTarjetaPago(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel, int idEmpresa)
        {
            return new TarjetaPago
            {
                idTarjetaPago = 0,
                idEmpresa = idEmpresa,
                nombreTitular = nuevoUsuarioEmpresaViewModel.nombreTitularTarjeta,
                numeroTarjeta = nuevoUsuarioEmpresaViewModel.numeroTarjeta,
                fechaExpiracion = nuevoUsuarioEmpresaViewModel.fechaExpiracionTarjeta,
                codigoSeguridad = nuevoUsuarioEmpresaViewModel.codigoSeguridadTarjeta,
                
            };
        }

        private UsuarioEmpresa CrearNuevoUsuario(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel, int idEmpresa)
        {
            return new UsuarioEmpresa
            {
                idUsuarioEmpresa = 0,
                idEmpresa = idEmpresa,
                idEstadoUsuario = ESTADO_USUARIO_PENDIENTE,
                fechaCreacion = System.DateTime.Now,
                fechaActualizacion = System.DateTime.Now,
                correo = nuevoUsuarioEmpresaViewModel.correo,
                clave = nuevoUsuarioEmpresaViewModel.clave,
            };
        }

        private Empresa CrearNuevaEmpresa(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel)
        {
            return new Empresa
            {
                idEmpresa = 0,
                idCiudad = nuevoUsuarioEmpresaViewModel.idCiudad,
                nif = nuevoUsuarioEmpresaViewModel.nif,
                nombre = nuevoUsuarioEmpresaViewModel.nombre,
                url = nuevoUsuarioEmpresaViewModel.url,
                numeroContacto = nuevoUsuarioEmpresaViewModel.numeroContacto,
                sector = nuevoUsuarioEmpresaViewModel.sector,
                direccion = nuevoUsuarioEmpresaViewModel.direccion
            };
        }

        private bool ExisteUnUsuarioConEseCorreoYEmpresa(string correo, int idEmpresa)
        {
            UsuarioEmpresa usuarioEmpresa =
                _usuarioEmpresaRepository.ObtenerUsuarioEmpresaValidoPorCorreoYEmpresa(
                    correo,
                    idEmpresa
                );
            if (usuarioEmpresa == null)
            {
                return false;
            }
            return true;
        }

        List<UsuarioEmpresaViewModel> IUsuarioEmpresaService.ObtenerUsuariosEmpresa()
        {
            return _usuarioEmpresaRepository.ObtenerUsuariosEmpresa();
        }

        public void ModificacionEstadoUsuarioEmpresa(int idUsuarioEmpresa, int idNuevoEstadoUsuario)
        {
            int estadoActualUsuarioEmpresa = _usuarioEmpresaRepository.ObtenerEstadoUsuarioEmpresa(idUsuarioEmpresa);

            if (estadoActualUsuarioEmpresa != idNuevoEstadoUsuario)
            {
                _usuarioEmpresaRepository.ModificarEstadoUsuarioEmpresa(idUsuarioEmpresa, idNuevoEstadoUsuario);
                EnviarCorreoActualizacionEstadoUsuarioEmpresa(idUsuarioEmpresa);
            }
            else
            {
                throw new Exception("No se ha cambiado el estado de usuario empresa");
            }  
        }

        private void EnviarCorreoActualizacionEstadoUsuarioEmpresa(int idUsuarioEmpresa)
        {
            DetalleUsuarioEmpresaViewModel usuarioEmpresa = _usuarioEmpresaRepository.ObtenerDetalleUsuarioEmpresa(idUsuarioEmpresa);
            if (usuarioEmpresa == null)
            {
                throw new Exception("No se encontró el usuario empresa con ese id");
            }

            NotificacionesCorreoHelper.EnviarCorreo(
                new List<string> { usuarioEmpresa.correo },
                "Actualización Usuario - ClubInflu",
                $@" Estimado/a <strong>{usuarioEmpresa.nombre}</strong>,<br /><br />
                    Le informamos que, tras la correspondiente verificación, su cuenta en
                    <strong>ClubInflu</strong> se encuentra actualmente en estado <strong>{usuarioEmpresa.estadoUsuario}</strong>.
                    <br /><br /> 
                    Para más información, por favor contáctese con nosotros al <strong>+1 (555) 123-4567</strong> o escriba a <strong>soporte@clubinflu.com</strong>.
                "
            );
        }

        public DetalleUsuarioEmpresaViewModel ObtenerDetalleUsuarioEmpresa(int idUsuarioEmpresa)
        {
            return _usuarioEmpresaRepository.ObtenerDetalleUsuarioEmpresa(idUsuarioEmpresa);
        }

        public Empresa ObtenerEmpresaPorIdUsuarioEmpresa(int idUsuarioEmpresa)
        {
            return _usuarioEmpresaRepository.ObtenerEmpresaPorIdUsuarioEmpresa(idUsuarioEmpresa);
        }
    }
}

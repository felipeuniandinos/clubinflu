using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Helpers;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class UsuarioInfluencerService : IUsuarioInfluencerService
    {
        public const int ESTADO_USUARIO_PENDIENTE = 1;
        private readonly IUsuarioInfluencerRepository _usuarioInfluencerRepository;

        public UsuarioInfluencerService(IUsuarioInfluencerRepository usuarioInfluencerRepository)
        {
            _usuarioInfluencerRepository = usuarioInfluencerRepository;
        }

        public int CrearUsuarioEmpresa(NuevoUsuarioInfluencerViewModel nuevoUsuarioInfluencerViewModel)
        {
            Influencer influencer = CrearInfluencer(nuevoUsuarioInfluencerViewModel);
            List<InfluencerRedSocial> redesSociales = CrearRedesSocialesInfluencer(nuevoUsuarioInfluencerViewModel);
            UsuarioInfluencer usuario = CrearNuevoUsuario(nuevoUsuarioInfluencerViewModel);


            if (ExisteUnUsuarioConEseCorreo(usuario.correo))
            {
                throw new Exception("Ya existe un usuario con ese correo registrado en el sistema");
            }

            usuario.clave = HashHelper.GenerarHash(usuario.clave);
            return _usuarioInfluencerRepository.CrearUsuarioInfluencer(usuario, influencer, redesSociales);
        }

        private Influencer CrearInfluencer(NuevoUsuarioInfluencerViewModel nuevoUsuarioInfluencerViewModel)
        {
            return
            new Influencer
            {
                idInfluencer = 0,
                idCiudad = nuevoUsuarioInfluencerViewModel.idCiudad,
                idCiudad2 = nuevoUsuarioInfluencerViewModel.idCiudad2,
                idCiudad3 = nuevoUsuarioInfluencerViewModel.idCiudad3,
                idCiudad4 = nuevoUsuarioInfluencerViewModel.idCiudad4,
                idGenero = nuevoUsuarioInfluencerViewModel.idGenero,
                nombre = nuevoUsuarioInfluencerViewModel.nombre,
                fechaNacimiento = nuevoUsuarioInfluencerViewModel.fechaNacimiento,
                numeroContacto = nuevoUsuarioInfluencerViewModel.numeroContacto
            };
        }

        private List<InfluencerRedSocial> CrearRedesSocialesInfluencer(NuevoUsuarioInfluencerViewModel nuevoUsuarioInfluencerViewModel)
        {
            DateTime fechaActual = System.DateTime.Now;
            List<InfluencerRedSocial> redesSociales = new List<InfluencerRedSocial>();
           
            foreach(NuevoInfluencerRedSocialViewModel redSocial in nuevoUsuarioInfluencerViewModel.redesSociales)
            {
                redesSociales.Add(new InfluencerRedSocial
                {
                    idInfluencerRedSocial = 0,
                    idInfluencer = 0,
                    idRedSocial = redSocial.idRedSocial,
                    numeroSeguidores = redSocial.numeroSeguidores,
                    fechaCreacion = fechaActual,
                    fechaActualizacion = fechaActual
                });
            }
            return redesSociales;
        }

        private UsuarioInfluencer CrearNuevoUsuario(NuevoUsuarioInfluencerViewModel nuevoUsuarioInfluencerViewModel)
        {
            return
            new UsuarioInfluencer
            {
                idUsuarioInfluencer = 0,
                idInfluencer = 0,
                idEstadoUsuario = ESTADO_USUARIO_PENDIENTE,
                fechaCreacion = System.DateTime.Now,
                fechaActualizacion = System.DateTime.Now,
                correo = nuevoUsuarioInfluencerViewModel.correo,
                clave = nuevoUsuarioInfluencerViewModel.clave,
            };
        }

        private bool ExisteUnUsuarioConEseCorreo(string correo)
        {
            UsuarioInfluencer usuarioInfluencer = _usuarioInfluencerRepository.ObtenerUsuarioInfluencerValidoPorCorreo(correo);
            if (usuarioInfluencer == null)
            {
                return false;
            }
            return true;
        }

        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer()
        {
            return _usuarioInfluencerRepository.ObtenerUsuariosInfluencer();
        }

        public GestionarUsuarioInfluencerViewModel GestionarUsuarioInfluencer(int idUsuarioInfluencer)
        {
            return _usuarioInfluencerRepository.GestionarUsuarioInfluencer(idUsuarioInfluencer);
        }

        public void ActualizarEstadoUsuarioInfluencer(int idUsuarioInfluencer, int idEstadoUsuarioInfluencer)
        {
            int estadoActualUsuarioInfluencer = _usuarioInfluencerRepository.ObtenerEstadoUsuarioInfluencer(idUsuarioInfluencer);

            if (estadoActualUsuarioInfluencer != idEstadoUsuarioInfluencer)
            {
                _usuarioInfluencerRepository.ActualizarEstadoUsuarioInfluencer(idUsuarioInfluencer, idEstadoUsuarioInfluencer);
                EnviarCorreoActualizacionEstadoUsuarioInfluencer(idUsuarioInfluencer);
            } else {
                throw new Exception("No se ha cambiado el estado de usuario influencer");
            }
            
        }

        private void EnviarCorreoActualizacionEstadoUsuarioInfluencer(int idUsuarioInfluencer)
        {
            GestionarUsuarioInfluencerViewModel usuarioInfluencer = _usuarioInfluencerRepository.GestionarUsuarioInfluencer(idUsuarioInfluencer);
            if (usuarioInfluencer == null)
            {
                throw new Exception("No se encontró el usuario influencer con ese id");
            }

            NotificacionesCorreoHelper.EnviarCorreo(
                new List<string> { usuarioInfluencer.correo },
                "Actualización Usuario - ClubInflu",
                $@" Estimado/a <strong>{usuarioInfluencer.nombre}</strong>,<br /><br />
                    Le informamos que, tras la correspondiente verificación, su cuenta en
                    <strong>ClubInflu</strong> se encuentra actualmente en estado <strong>{usuarioInfluencer.estadoUsuario}</strong>.
                    <br /><br /> 
                    Para más información, por favor contáctese con nosotros al <strong>+1 (555) 123-4567</strong> o escriba a <strong>soporte@clubinflu.com</strong>.
                "
            );
        }
    }
}

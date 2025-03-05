using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
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


            if (ExisteUnUsuarioConEseCorreoYEmpresa(usuario.correo, empresa.idEmpresa))
            {
                throw new Exception("Ya existe un usuario asociado a la empresa con ese correo registrado en el sistema");
            }

            return _usuarioEmpresaRepository.CrearUsuarioEmpresa(usuario, empresa);
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
        

        private UsuarioEmpresa CrearNuevoUsuario(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel, int idEmpresa)
        {
            return
            new UsuarioEmpresa
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
            return
            new Empresa
            {
                idEmpresa = 0,
                idCiudad = nuevoUsuarioEmpresaViewModel.idCiudad,
                idCiudad2 = nuevoUsuarioEmpresaViewModel.idCiudad2,
                idCiudad3 = nuevoUsuarioEmpresaViewModel.idCiudad3,
                idCiudad4 = nuevoUsuarioEmpresaViewModel.idCiudad4,
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
            UsuarioEmpresa usuarioEmpresa = _usuarioEmpresaRepository.ObtenerUsuarioEmpresaValidoPorCorreoYEmpresa(correo, idEmpresa);
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
    }
}

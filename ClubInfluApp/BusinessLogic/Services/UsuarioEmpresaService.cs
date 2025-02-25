using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class UsuarioEmpresaService : IUsuarioEmpresaService
    {
        public const int ESTADO_USUARIO_PENDIENTE = 3;
        private readonly IUsuarioEmpresaRepository _usuarioEmpresaRepository;

        public UsuarioEmpresaService(IUsuarioEmpresaRepository usuarioEmpresaRepository)
        {
            _usuarioEmpresaRepository = usuarioEmpresaRepository;
        }

        public int CrearUsuarioEmpresa(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel)
        {
            UsuarioEmpresa usuarioEmpresa = MapearUsuarioEmpresaDesdeNuevoUsuarioEmpresaViewModel(nuevoUsuarioEmpresaViewModel);
            Empresa empresa = MapearEmpresaDesdeNuevoUsuarioEmpresaViewModel(nuevoUsuarioEmpresaViewModel);

            return _usuarioEmpresaRepository.CrearUsuarioEmpresa(usuarioEmpresa, empresa);
        }

        private UsuarioEmpresa MapearUsuarioEmpresaDesdeNuevoUsuarioEmpresaViewModel(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresa)
        {
            return
            new UsuarioEmpresa
            {
                idUsuarioEmpresa = 0,
                idEmpresa = 0,
                idEstadoUsuario = ESTADO_USUARIO_PENDIENTE,
                fechaCreacion = System.DateTime.Now,
                fechaActualizacion = System.DateTime.Now,
                correo = nuevoUsuarioEmpresa.correo,
                clave = nuevoUsuarioEmpresa.clave,

            };
        }

        private Empresa MapearEmpresaDesdeNuevoUsuarioEmpresaViewModel(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresa)
        {
            return
            new Empresa
            {
                idEmpresa = 0,
                idCiudad = nuevoUsuarioEmpresa.idCiudadEmpresa,
                nombre = nuevoUsuarioEmpresa.nombreEmpresa,
                url = nuevoUsuarioEmpresa.urlEmpresa,
                numeroContacto = nuevoUsuarioEmpresa.numeroContactoEmpresa,
                sector = nuevoUsuarioEmpresa.sectorEmpresa,
                direccion = nuevoUsuarioEmpresa.direccionEmpresa
            };
        }
    }
}

using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public int ObtenerIdUsuario(UsuarioViewModel usuarioViewModel)
        {
            switch (usuarioViewModel.tipo)
            {
                case TipoUsuario.Empresa:
                    return _usuarioRepository.AutenticarUsuarioEmpresa(usuarioViewModel.correo, usuarioViewModel.clave);
                case TipoUsuario.Influencer:
                    return _usuarioRepository.AutenticarUsuarioInfluencer(usuarioViewModel.correo, usuarioViewModel.clave);
                case TipoUsuario.Administrador:
                    return _usuarioRepository.AutenticarUsuarioAdministrador(usuarioViewModel.correo, usuarioViewModel.clave);
                default:
                    return 0;
            }
        }
    }
}

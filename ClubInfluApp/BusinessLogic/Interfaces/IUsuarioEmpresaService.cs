using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface IUsuarioEmpresaService
    {
        public int CrearUsuarioEmpresa(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel);

        public List<UsuarioEmpresaViewModel> ObtenerUsuariosEmpresa();

        public void ModificacionEstadoUsuarioEmpresa(int idUsuarioEmpresa, int idNuevoEstadoUsuario);

        public DetalleUsuarioEmpresaViewModel ObtenerDetalleUsuarioEmpresa(int idUsuarioEmpresa);

        public Empresa ObtenerEmpresaPorIdUsuarioEmpresa(int idUsuarioEmpresa);
    }
}

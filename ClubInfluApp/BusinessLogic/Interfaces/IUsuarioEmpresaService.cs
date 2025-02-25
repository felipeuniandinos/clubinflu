using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface IUsuarioEmpresaService
    {
        public int CrearUsuarioEmpresa(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel);

        public List<UsuarioEmpresaViewModel> ObtenerUsuariosEmpresa();
    }
}

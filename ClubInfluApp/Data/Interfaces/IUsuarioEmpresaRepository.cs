using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUsuarioEmpresaRepository
    {
        public int CrearUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa, Empresa empresa);

        public UsuarioEmpresa ObtenerUsuarioEmpresaValidoPorCorreoYEmpresa(string correo, int idEmpresa);

        public Empresa ObtenerEmpresaPorNif(string nif);

        public List<UsuarioEmpresaViewModel> ObtenerUsuariosEmpresa();
    }
}

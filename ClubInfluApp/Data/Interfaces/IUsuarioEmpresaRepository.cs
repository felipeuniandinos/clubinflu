using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUsuarioEmpresaRepository
    {
        public int CrearUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa, Empresa empresa, TarjetaPago tarjetaPago);

        public UsuarioEmpresa ObtenerUsuarioEmpresaValidoPorCorreoYEmpresa(string correo, int idEmpresa);

        public Empresa ObtenerEmpresaPorNif(string nif);

        public List<UsuarioEmpresaViewModel> ObtenerUsuariosEmpresa();

        public void ActualizarEstadoUsuarioEmpresa(int idUsuarioEmpresa, int idNuevoEstadoUsuario);

        public GestionarUsuarioEmpresaViewModel GestionarUsuarioEmpresa(int idUsuarioEmpresa);

        public int ObtenerEstadoUsuarioEmpresa(int idUsuarioEmpresa);

        public Empresa ObtenerEmpresaPorIdUsuarioEmpresa(int idUsuarioEmpresa);
    }
}

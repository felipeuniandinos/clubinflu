using ClubInfluApp.Models;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUsuarioEmpresaRepository
    {
        public UsuarioEmpresa CrearUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa, Empresa empresa, int idCiudad);
    }
}

using ClubInfluApp.Models;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUsuarioEmpresaRepository
    {
        public int CrearUsuarioEmpresa(UsuarioEmpresa usuarioEmpresa, Empresa empresa);
    }
}

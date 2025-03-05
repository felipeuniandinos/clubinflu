using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUsuarioRepository
    {
        public int AutenticarUsuarioEmpresa(string correo, string clave);
        public int AutenticarUsuarioInfluencer(string correo, string clave);
        public int AutenticarUsuarioAdministrador(string correo, string clave);
    }
}

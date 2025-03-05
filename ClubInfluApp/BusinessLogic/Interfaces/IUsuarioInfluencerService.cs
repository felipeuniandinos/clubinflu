using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface IUsuarioInfluencerService
    {
        public int CrearUsuarioEmpresa(NuevoUsuarioInfluencerViewModel nuevoUsuarioInfluencerViewModel);

        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer();
    }
}

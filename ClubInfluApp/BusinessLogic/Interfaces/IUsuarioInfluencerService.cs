using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface IUsuarioInfluencerService
    {
        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer();
        public GestionarUsuarioInfluencerViewModel GestionarUsuarioInfluencer(int idUsuarioInfluencer);
    }
}

using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUsuarioInfluencerRepository
    {
        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer();
    }
}

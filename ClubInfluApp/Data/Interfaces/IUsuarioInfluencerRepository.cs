using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUsuarioInfluencerRepository
    {
        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer();
        public GestionarUsuarioInfluencerViewModel GestionarUsuarioInfluencer(int idUsuarioInfluencer);
        public void ActualizarEstadoUsuarioInfluencer(int idUsuarioInfluencer, int idEstadoUsuarioInfluencer);
    }
}

using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface IUsuarioInfluencerService
    {
        public int CrearUsuarioEmpresa(NuevoUsuarioInfluencerViewModel nuevoUsuarioInfluencerViewModel);

        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer();
        
        public GestionarUsuarioInfluencerViewModel GestionarUsuarioInfluencer(int idUsuarioInfluencer);
        
        public void ActualizarEstadoUsuarioInfluencer(int idUsuarioInfluencer, int idEstadoUsuarioInfluencer);

        public Influencer ObtenerInfluencerPorIdUsuarioInfluencer(int idUsuarioInfluencer);
    }
}

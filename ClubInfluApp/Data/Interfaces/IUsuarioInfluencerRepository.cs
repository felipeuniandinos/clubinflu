using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUsuarioInfluencerRepository
    {
        public int CrearUsuarioInfluencer(UsuarioInfluencer usuarioInfluencer, Influencer influencer, List<InfluencerRedSocial> influencerRedesSociales);

        public UsuarioInfluencer ObtenerUsuarioInfluencerValidoPorCorreo(string correo);

        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer();
        public GestionarUsuarioInfluencerViewModel GestionarUsuarioInfluencer(int idUsuarioInfluencer);
        public void ActualizarEstadoUsuarioInfluencer(int idUsuarioInfluencer, int idEstadoUsuarioInfluencer);
    }
}

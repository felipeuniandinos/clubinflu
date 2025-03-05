using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IUsuarioInfluencerRepository
    {
        public int CrearUsuarioInfluencer(UsuarioInfluencer usuarioInfluencer, Influencer influencer, List<InfluencerRedSocial> influencerRedesSociales);

        public UsuarioInfluencer ObtenerUsuarioInfluencerValidoPorCorreo(string correo);

        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer();
    }
}

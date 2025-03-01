using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class UsuarioInfluencerService : IUsuarioInfluencerService
    {
        private readonly IUsuarioInfluencerRepository _usuarioInfluencerRepository;

        public UsuarioInfluencerService(IUsuarioInfluencerRepository usuarioInfluencerRepository)
        {
            _usuarioInfluencerRepository = usuarioInfluencerRepository;
        }

        public List<UsuarioInfluencerViewModel> ObtenerUsuariosInfluencer()
        {
            //TODO: Implementar
            return _usuarioInfluencerRepository.ObtenerUsuariosInfluencer();
        }
    }
}

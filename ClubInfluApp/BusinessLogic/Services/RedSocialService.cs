using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class RedSocialService : IRedSocialService
    {
        private readonly IRedSocialRepository _redSocialRepository;

        public RedSocialService(IRedSocialRepository redSocialRepository)
        {
            _redSocialRepository = redSocialRepository;
        }
        
        public List<RedSocial> ObtenerRedesSociales()
        {
            return _redSocialRepository.ObtenerRedesSociales();
        }
    }
}

using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class RedSocialService : IRedSocialService
    {
        private readonly IRedSocialRepository redSocialRepository;
        public RedSocialService(IRedSocialRepository redSocialRepository)
        {
            this.redSocialRepository = redSocialRepository;
        }
        public List<RedSocial> ObtenerRedesSociales()
        {
            return redSocialRepository.ObtenerRedesSociales();
        }
    }
}

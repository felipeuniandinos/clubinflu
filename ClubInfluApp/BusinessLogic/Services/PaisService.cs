using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class PaisService : IPaisService
    {
        private readonly IPaisRepository _paisRepository;
        public PaisService(IPaisRepository paisRepository)
        {
            _paisRepository = paisRepository;
        }
        public List<Pais> ObtenerPaises()
        {
            return _paisRepository.ObtenerPaises();
        }
    }
}

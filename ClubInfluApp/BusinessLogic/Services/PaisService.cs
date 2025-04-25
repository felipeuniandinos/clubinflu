using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class PaisService : IPaisService
    {
        private readonly IPaisRepository paisRepository;
        public PaisService(IPaisRepository paisRepository)
        {
            this.paisRepository = paisRepository;
        }
        public List<Pais> ObtenerPaises()
        {
            return paisRepository.ObtenerPaises();
        }
    }
}

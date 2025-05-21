using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class CuponServicioService : ICuponServicioService
    {
        private readonly ICuponServicioRepository _cuponServicioRepository;

        public CuponServicioService(ICuponServicioRepository cuponServicioRepository)
        {
            _cuponServicioRepository = cuponServicioRepository;
        }
        public List<CuponServicioViewModel> ObtenerCuponesPorOfertaServicio(int idOfertaServicio)
        {
            return _cuponServicioRepository.ObtenerCuponesPorOfertaServicio(idOfertaServicio);
        }
    }
}

using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Data.Repositories;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services

{
    public class CuponServicioService: ICuponServicioService
    {
        private readonly ICuponServicioRepository _cuponServicioRepository;

        public CuponServicioService(ICuponServicioRepository cuponServicioRepository)
        {
            _cuponServicioRepository = cuponServicioRepository;
        }

        public void ReservarCuponOfertaServicio(CuponServicioViewModel cuponServicioViewModel)
        {
            if (cuponServicioViewModel == null)
            {
                _cuponServicioRepository.ReservarCuponOfertaServicio(cuponServicioViewModel);
            }
            else
            {
                throw new Exception("No se ha realizado la reserva de cupon de servicio");
            }

        }

    }
}

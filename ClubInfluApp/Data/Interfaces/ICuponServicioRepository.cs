using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface ICuponServicioRepository
    {
        public void ReservarCuponOfertaServicio(CuponServicioViewModel cuponServicioViewModel);
    }
}

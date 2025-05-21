using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface ICuponServicioService
    {
        public List<CuponServicioViewModel> ObtenerCuponesPorOfertaServicio(int idOfertaServicio);
    }
}

using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface ICuponServicioService
    {
        public void ReservarCuponOfertaServicio(int idOfertaServicio);

        public List<CuponServicioViewModel> ObtenerCuponesPorOfertaServicio(int idOfertaServicio);

        public List<CuponServicioViewModel> ListarCuponesServicioPorInfluencer();
    }
}

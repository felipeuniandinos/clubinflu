using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface ICuponServicioRepository
    {
        public List<CuponServicioViewModel> ObtenerCuponesPorOfertaServicio(int idOfertaServicio);
    }
}

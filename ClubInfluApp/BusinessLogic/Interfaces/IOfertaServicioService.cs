using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface IOfertaServicioService
    {
        public OfertasServiciosViewModel ObtenerOfertasDeServicioFiltradas(FiltroOfertasDeServicio filtroOfertasDeServicio);
    }
}

using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.Data.Interfaces
{
    public interface IOfertaServicioRepository
    {
        public List<OfertaServicioViewModel> ObtenerOfertasDeServicioFiltradas(FiltroOfertasDeServicio filtroOfertasDeServicio);

        public int CrearOfertaDeServicio(OfertaServicio ofertaServicio);
    }
}

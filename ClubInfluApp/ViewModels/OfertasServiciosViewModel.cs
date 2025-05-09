using ClubInfluApp.Models;

namespace ClubInfluApp.ViewModels
{
    public class OfertasServiciosViewModel
    {
        public List<CategoriaOferta> categorias { get; set; }
        public List<Pais> paises { get; set; }
        public List<OfertaServicioViewModel> ofertasServicios { get; set; }
    }
}

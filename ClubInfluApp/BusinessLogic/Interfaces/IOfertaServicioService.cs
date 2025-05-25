using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Interfaces
{
    public interface IOfertaServicioService
    {
        public OfertasServiciosViewModel ObtenerOfertasDeServicioFiltradas(FiltroOfertasDeServicio filtroOfertasDeServicio);
        public List<OfertaServicioViewModel> ObtenerOfertasDeServicioPorEmpresa();
        public int CrearOfertaServicio(NuevaOfertaServicioViewModel nuevaOfertaServicioViewModel);
        public string validarCuponDeServicioPorCodigo(string codigoDeCuponAValidar);
    }
}

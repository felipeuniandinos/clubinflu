using System.Collections.Generic;
using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;

namespace ClubInfluApp.BusinessLogic.Services
{
    public class OfertaServicioService : IOfertaServicioService
    {
        private readonly IOfertaServicioRepository _ofertaServicioRepository;
        private readonly IPaisService _paisService;
        private readonly ICategoriaService _categoriaService;
        public OfertaServicioService(IOfertaServicioRepository ofertaServicioRepository, IPaisService paisService, ICategoriaService categoriaService)
        {
            _ofertaServicioRepository = ofertaServicioRepository;
            _paisService = paisService;
            _categoriaService = categoriaService;
        }
        public OfertasServiciosViewModel ObtenerOfertasDeServicioFiltradas(FiltroOfertasDeServicio filtroOfertasDeServicio)
        {
            OfertasServiciosViewModel ofertaServicioViewModel = new OfertasServiciosViewModel();
            List<OfertaServicioViewModel> ofertasDeServicioFiltradas = _ofertaServicioRepository.ObtenerOfertasDeServicioFiltradas(filtroOfertasDeServicio);
            ofertaServicioViewModel.ofertasServicios = ofertasDeServicioFiltradas;
            ofertaServicioViewModel.paises = _paisService.ObtenerPaises();
            ofertaServicioViewModel.categorias = _categoriaService.ObtenerCategorias();

            return ofertaServicioViewModel;
        }
    }
}

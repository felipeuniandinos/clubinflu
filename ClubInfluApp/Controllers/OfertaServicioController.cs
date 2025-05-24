using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class OfertaServicioController : Controller
    {
        private readonly IOfertaServicioService _ofertaServicioService;
        private readonly ICategoriaService _categoriaService;
        private readonly ICuponServicioService _cuponServicioService;

        public OfertaServicioController(IOfertaServicioService ofertaServicioService, ICategoriaService categoriaService, ICuponServicioService cuponServicioService)
        {
            _ofertaServicioService = ofertaServicioService;
            _categoriaService = categoriaService;
            _cuponServicioService = cuponServicioService;
        }

        [HttpGet]
        [Authorize(Roles = "Influencer")]
        public IActionResult ListarOfertasDeServicio(FiltroOfertasDeServicio filtroOfertasDeServicio)
        {
            OfertasServiciosViewModel ofertasServicios = _ofertaServicioService.ObtenerOfertasDeServicioFiltradas(filtroOfertasDeServicio);
            return View(ofertasServicios);
        }

        [HttpGet]
        [Authorize(Roles = "Empresa")]
        public IActionResult HistoricoOfertasDeServicioEmpresa()
        {
            List<OfertaServicioViewModel> ofertasServiciosPorEmpresa = _ofertaServicioService.ObtenerOfertasDeServicioPorEmpresa();
            return View(ofertasServiciosPorEmpresa);
        }

        [HttpGet]
        [Authorize(Roles = "Empresa")]
        public IActionResult DetalleCupones(int idOfertaServicio)
        {
            List<CuponServicioViewModel> cuponesServicio = _cuponServicioService.ObtenerCuponesPorOfertaServicio(idOfertaServicio);

            return View(cuponesServicio);
        }

        [HttpGet]
        [Authorize(Roles = "Empresa")]
        public IActionResult CrearOfertaDeServicio()
        {
            NuevaOfertaServicioViewModel nuevaOfertaServicioViewModel = new NuevaOfertaServicioViewModel();
            nuevaOfertaServicioViewModel.categorias = _categoriaService.ObtenerCategorias();
            return View(nuevaOfertaServicioViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Empresa")]
        [ValidateAntiForgeryToken]
        public IActionResult CrearOfertaDeServicio(NuevaOfertaServicioViewModel nuevaOfertaServicioViewModel)
        {
            nuevaOfertaServicioViewModel.categorias = _categoriaService.ObtenerCategorias();
            ModelState.Remove("categorias");

            if (!ModelState.IsValid)
            {
                return View(nuevaOfertaServicioViewModel);
            }

            int idOfertaServico = _ofertaServicioService.CrearOfertaServicio(nuevaOfertaServicioViewModel);
            ViewBag.Mensaje = "La oferta fue creada correctamente.";
            return View(nuevaOfertaServicioViewModel);
        }
        
        [HttpGet]
        [Authorize(Roles = "Influencer")]
        public IActionResult ListarCuponesServicio(int idInfluencer)
        {
            List<CuponServicioViewModel> cuponesServicio = _cuponServicioService.ListarCuponesServicioPorInfluencer();
            return View(cuponesServicio);
        }
        
    }
}

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

        public OfertaServicioController(IOfertaServicioService ofertaServicioService)
        {
            _ofertaServicioService = ofertaServicioService;
        }

        [Authorize(Roles = "Influencer")]
        public IActionResult ListarOfertasDeServicio(FiltroOfertasDeServicio filtroOfertasDeServicio)
        {
            OfertasServiciosViewModel ofertasServicios = _ofertaServicioService.ObtenerOfertasDeServicioFiltradas(filtroOfertasDeServicio);
            return View(ofertasServicios);
        }

        [Authorize(Roles = "Empresa")]
        public IActionResult HistoricoCuponesDeServicio()
        {
            return View();
        }
    }
}

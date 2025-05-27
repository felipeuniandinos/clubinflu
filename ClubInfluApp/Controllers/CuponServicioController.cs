using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.BusinessLogic.Services;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class CuponServicioController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly ICuponServicioService _cuponServicioService;

        public CuponServicioController(ILogger<InicioController> logger, ICuponServicioService cuponServicioService)
        {
            _logger = logger;
            _cuponServicioService = cuponServicioService;
        }

 
        [HttpPut]
        [Authorize(Roles = "Influencer")]
        public IActionResult ReservarCuponOfertaServicio(int idOfertaServicio)
        {
            try
            {
                _cuponServicioService.ReservarCuponOfertaServicio(idOfertaServicio);
                return Json(new { exito = true, mensaje = "La reserva de cupon se ha realizado con exito, el código de reserva se ha enviado a su correo" });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, error = ex.Message });
            }
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

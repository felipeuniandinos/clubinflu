using System.Text.Json;
using System.Runtime.InteropServices;
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

        [HttpPut]
        [Authorize(Roles = "Empresa")]
        public IActionResult validarCuponDeServicioPorCodigo(string codigoDeCuponAValidar)
        {
            try
            {
                string mensaje = _cuponServicioService.validarCuponDeServicioPorCodigo(codigoDeCuponAValidar);
                return Json(new { exito = true, mensaje = mensaje });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, error = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Influencer")]
        public IActionResult ListarCuponesServicio()
        {
            List<string> cuponesPorFinalizar = _cuponServicioService.ObtenerCuponesPorFinalizar();

            if (cuponesPorFinalizar.Count > 0)
            {
                ViewData["CuponesPendientes"] = "Tiene cupones de servicio por finalizar, por favor finalice los cupones antes de reservar nuevos:   " +
                    string.Join(", ", cuponesPorFinalizar);
            }
           
            List<CuponServicioViewModel> cuponesServicio = _cuponServicioService.ListarCuponesServicioPorInfluencer();
            return View(cuponesServicio);
        }

        [HttpGet]
        [Authorize(Roles = "Influencer")]
        public IActionResult GestionarCuponServicio(int idCuponServicio, IFormFile video)
        {
            CuponServicioViewModel cuponServicioPorIdCuponServicio = _cuponServicioService.ObtenerCuponServicioPorIdCuponServicio(idCuponServicio);
            if (cuponServicioPorIdCuponServicio == null)
            {
                return NotFound();
            }
            return View(cuponServicioPorIdCuponServicio);
        }

    
        public IActionResult SubirVideoCuponServicio(int idCuponServicio, IFormFile video)
        {
            CuponServicioViewModel cuponServicioVideo = _cuponServicioService.SubirVideoCuponServicio(idCuponServicio, video);
            if (cuponServicioVideo == null)
            {
                return NotFound();
            }
            return RedirectToAction("GestionarCuponServicio", new { idCuponServicio = cuponServicioVideo.idCuponServicio });
        }

    }
}

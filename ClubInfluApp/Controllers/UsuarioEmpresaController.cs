using System.Reflection;
using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.BusinessLogic.Services;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class UsuarioEmpresaController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly IUsuarioEmpresaService _usuarioEmpresaService;
        private readonly IPaisService _paisService;
        private readonly IEstadoService _estadoService;
        private readonly ICiudadService _ciudadService;

        public UsuarioEmpresaController(ILogger<InicioController> logger, IUsuarioEmpresaService usuarioEmpresaService, IPaisService paisService, IEstadoService estadoService, ICiudadService ciudadService)
        {
            _logger = logger;
            _usuarioEmpresaService = usuarioEmpresaService;
            _paisService = paisService;
            _estadoService = estadoService;
            _ciudadService = ciudadService;
        }

        [Authorize(Roles = "Administrador")]

        [HttpGet]
        public IActionResult ListarUsuariosEmpresa()
        {
            List<UsuarioEmpresaViewModel> usuariosEmpresa = _usuarioEmpresaService.ObtenerUsuariosEmpresa();
            return View(usuariosEmpresa);
        }

        [HttpGet]
        public IActionResult CrearUsuarioEmpresa()
        {
            NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel = new NuevoUsuarioEmpresaViewModel();
            nuevoUsuarioEmpresaViewModel.paises = _paisService.ObtenerPaises();
            nuevoUsuarioEmpresaViewModel.fechaExpiracionTarjeta = null;


            return View(nuevoUsuarioEmpresaViewModel);
        }

        [HttpPost]
        public IActionResult CrearUsuarioEmpresa(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel)
        {
            nuevoUsuarioEmpresaViewModel.paises = _paisService.ObtenerPaises();
            ModelState.Remove("paises");
           
            if (!ModelState.IsValid)
            {
                return View(nuevoUsuarioEmpresaViewModel);
            }

            int idUsuarioEmpresa = _usuarioEmpresaService.CrearUsuarioEmpresa(nuevoUsuarioEmpresaViewModel);
            ViewBag.Mensaje = "Registro completado. Revisaremos tu información y nos pondremos en contacto pronto.El equipo de Club Influ";
            return View(nuevoUsuarioEmpresaViewModel);
        }

        [HttpGet]
        public IActionResult GestionarUsuarioEmpresa(int idUsuarioEmpresa)
        {
            DetalleUsuarioEmpresaViewModel detalleUsuarioEmpresa = _usuarioEmpresaService.ObtenerDetalleUsuarioEmpresa(idUsuarioEmpresa);
            return View(detalleUsuarioEmpresa);
        }

        [HttpPut]
        public IActionResult ModificarEstadoUsuarioEmpresa(int idUsuarioEmpresa, int idNuevoEstadoUsuario)
        {
            try
            {
                _usuarioEmpresaService.ModificacionEstadoUsuarioEmpresa(idUsuarioEmpresa, idNuevoEstadoUsuario);
                return Json(new { exito = true, mensaje = "El usuario empresa esta actualizado con exito" });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, error = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult ObtenerCiudadesPorEstadoYTermino(int idEstado, string termino)
        {
            try
            {
                List<Ciudad> ciudades = _ciudadService.ObtenerCiudadesPorEstadoYTermino(idEstado, termino);
                return Json(new { exito = true, data = ciudades });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, error = ex.Message });
            }
        }

        [HttpGet]
        public JsonResult ObtenerEstadosPorPaisYTermino(int idPais, string termino)
        {  
            try
            {
                List<Estado> estados = _estadoService.ObtenerEstadosPorPaisYTermino(idPais, termino);
                return Json(new { exito = true, data = estados });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, error = ex.Message });
            }
        }
    }
}

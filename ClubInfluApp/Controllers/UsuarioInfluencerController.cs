using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.BusinessLogic.Services;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class UsuarioInfluencerController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly IUsuarioInfluencerService _usuarioInfluencerService;
        private readonly IPaisService _paisService;
        private readonly ICiudadService _ciudadService;
        private readonly IEstadoService _estadoService;

        public UsuarioInfluencerController(ILogger<InicioController> logger, IUsuarioInfluencerService usuarioInfluencerService, IPaisService paisService, ICiudadService ciudadService, IEstadoService estadoService)
        {
            _logger = logger;
            _usuarioInfluencerService = usuarioInfluencerService;
            _paisService = paisService;
            _ciudadService = ciudadService;
            _estadoService = estadoService;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult ListarUsuariosInfluencer()
        {
            List<UsuarioInfluencerViewModel> influencers = _usuarioInfluencerService.ObtenerUsuariosInfluencer();
            return View(influencers);
        }

        [HttpGet]
        public IActionResult CrearUsuarioInfluencer()
        {
            NuevoUsuarioInfluencerViewModel nuevoUsuarioInfluencerViewModel = new NuevoUsuarioInfluencerViewModel();
            nuevoUsuarioInfluencerViewModel.paises = _paisService.ObtenerPaises();
            return View(nuevoUsuarioInfluencerViewModel);
        }

        [HttpPost]
        public IActionResult CrearUsuarioInfluencer(NuevoUsuarioInfluencerViewModel nuevoUsuarioInfluencerViewModel)
        {
            nuevoUsuarioInfluencerViewModel.paises = _paisService.ObtenerPaises();
            ModelState.Remove("paises");

            if (!ModelState.IsValid)
            {
                return View(nuevoUsuarioInfluencerViewModel);
            }

            int idUsuarioEmpresa = _usuarioInfluencerService.CrearUsuarioEmpresa(nuevoUsuarioInfluencerViewModel);
           
            ViewBag.Mensaje = "Registro completado. Revisaremos tu información y nos pondremos en contacto pronto. El equipo de Club influ.";
            return View(nuevoUsuarioInfluencerViewModel);
        }

        [HttpGet]
        public IActionResult GestionarSolicitudesUsuarioInfluencer(int idUsuarioInfluencer)
        {
            
            
            GestionarUsuarioInfluencerViewModel detalleUsuarioInfluencer = _usuarioInfluencerService.GestionarUsuarioInfluencer(idUsuarioInfluencer);

            if (detalleUsuarioInfluencer == null)
            {
                return NotFound();
            }

            return View(detalleUsuarioInfluencer);

        }

        [HttpPut]
        public IActionResult ActualizarEstadoUsuarioInfluencer (int idUsuarioInfluencer, int idEstadoUsuarioInfluencer)
        {
            _usuarioInfluencerService.ActualizarEstadoUsuarioInfluencer(idUsuarioInfluencer, idEstadoUsuarioInfluencer);

            return Json(new { mensaje = "El usuario influencer esta actualizado con exito" });
        }

        [HttpGet]
        public JsonResult ObtenerCiudadesPorEstadoYTermino(int idEstado, string termino)
        {
            List<Ciudad> ciudades = _ciudadService.ObtenerCiudadesPorEstadoYTermino(idEstado, termino);

            return Json(ciudades);
        }

        [HttpGet]
        public JsonResult ObtenerEstadosPorPaisYTermino(int idPais, string termino)
        {
            List<Estado> estados = _estadoService.ObtenerEstadosPorPaisYTermino(idPais, termino);

            return Json(estados);
        }
    }
}

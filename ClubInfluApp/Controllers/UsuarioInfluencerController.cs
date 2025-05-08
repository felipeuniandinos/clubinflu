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
        private readonly IGeneroService _generoService;
        private readonly IRedSocialService _redSocialService;

        public UsuarioInfluencerController(ILogger<InicioController> logger, IUsuarioInfluencerService usuarioInfluencerService, IPaisService paisService, ICiudadService ciudadService, IEstadoService estadoService, IGeneroService generoService, IRedSocialService redSocialService)
        {
            _logger = logger;
            _usuarioInfluencerService = usuarioInfluencerService;
            _paisService = paisService;
            _ciudadService = ciudadService;
            _estadoService = estadoService;
            _generoService = generoService;
            _redSocialService = redSocialService;
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
            nuevoUsuarioInfluencerViewModel.generos = _generoService.ObtenerGeneros();
            nuevoUsuarioInfluencerViewModel.fechaNacimiento = null;
            return View(nuevoUsuarioInfluencerViewModel);
        }

        [HttpPost]
        public IActionResult CrearUsuarioInfluencer(NuevoUsuarioInfluencerViewModel nuevoUsuarioInfluencerViewModel)
        {
            nuevoUsuarioInfluencerViewModel.paises = _paisService.ObtenerPaises();
            nuevoUsuarioInfluencerViewModel.generos = _generoService.ObtenerGeneros();
            ModelState.Remove("paises");
            ModelState.Remove("generos");
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
        public IActionResult ActualizarEstadoUsuarioInfluencer(int idUsuarioInfluencer, int idEstadoUsuarioInfluencer)
        {
            try
            {
                _usuarioInfluencerService.ActualizarEstadoUsuarioInfluencer(idUsuarioInfluencer, idEstadoUsuarioInfluencer);
                return Json(new { exito = true, mensaje = "El usuario influencer esta actualizado con exito" });
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

       
        [HttpGet]
        public JsonResult ObtenerRedesSociales()
        {
            try
            {
                List<RedSocial> redSociales = _redSocialService.ObtenerRedesSociales();
                return Json(new { exito = true, data = redSociales });
            }
            catch (Exception ex)
            {
                return Json(new { exito = false, error = ex.Message });
            }
        }
    }
}

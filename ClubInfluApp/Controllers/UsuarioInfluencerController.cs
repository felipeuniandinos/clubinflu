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
            //TODO: Obtener los generos (Recuerda creas el repositorio y el servicio -- No olvides la intefaces y ponerlas en el program)
            // nuevoUsuarioInfluencerViewModel.generos = _generoService.ObtenerGeneros();
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
            List<Ciudad> ciudades = _ciudadService.ObtenerCiudadesPorEstadoYTermino(idEstado, termino);

            return Json(ciudades);
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

        //TODO: Crear el metodo para obtener las redes sociales
        [HttpGet]
        public JsonResult ObtenerRedesSociales()
        {
            //(Recuerda creas el repositorio y el servicio -- No olvides la intefaces y ponerlas en el program)
            //List<RedSocial> redSociales = _redSocialService.ObtenerRedesSociales();
            List<RedSocial> redSociales = new List<RedSocial>
            {
                new RedSocial { idRedSocial = 1, redSocial = "Instagram" },
                new RedSocial { idRedSocial = 2, redSocial = "Facebook" },
                new RedSocial {idRedSocial = 3, redSocial = "Twitter"},
                new RedSocial {idRedSocial = 4, redSocial = "TikTok"}
            };
            return Json(redSociales);
        }
    }
}

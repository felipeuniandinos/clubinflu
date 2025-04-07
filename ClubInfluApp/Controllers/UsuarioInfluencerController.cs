using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class UsuarioInfluencerController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly IUsuarioInfluencerService _usuarioInfluencerService;

        public UsuarioInfluencerController(ILogger<InicioController> logger, IUsuarioInfluencerService usuarioInfluencerService)
        {
            _logger = logger;
            _usuarioInfluencerService = usuarioInfluencerService;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public IActionResult ListarUsuarioInfluencer()
        {
            List<UsuarioInfluencerViewModel> influencers = _usuarioInfluencerService.ObtenerUsuariosInfluencer();
            return View(influencers);
        }

        [HttpGet]
        public IActionResult CrearUsuarioInfluencer()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuarioInfluencer(NuevoUsuarioInfluencerViewModel nuevoUsuarioInfluencerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nuevoUsuarioInfluencerViewModel);
            }

            int idUsuarioEmpresa = _usuarioInfluencerService.CrearUsuarioEmpresa(nuevoUsuarioInfluencerViewModel);

            ViewBag.Mensaje = "Registro completado. Revisaremos tu información y nos pondremos en contacto pronto. El equipo de Club influ.";
            return View();
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
    }
}

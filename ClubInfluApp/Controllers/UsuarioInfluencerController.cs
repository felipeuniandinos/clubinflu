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
        public IActionResult ListarUsuariosInfluencer()
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

            ViewBag.Mensaje = "Usuario creado exitosamente. Con el id:" + idUsuarioEmpresa;
            return View();
        }
    }
}

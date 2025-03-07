using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class UsuarioInfluencerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioInfluencerService _usuarioInfluencerService;

        public UsuarioInfluencerController(ILogger<HomeController> logger, IUsuarioInfluencerService usuarioInfluencerService)
        {
            _logger = logger;
            _usuarioInfluencerService = usuarioInfluencerService;
        }

        [HttpGet]
        public IActionResult ListarUsuarioInfluencer()
        {
            try
            {
                List<UsuarioInfluencerViewModel> influencers = _usuarioInfluencerService.ObtenerUsuariosInfluencer();
                return View(influencers);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error al obtener los usuarios influencers: {ex.Message}");
                return View("Error");
            }
        }

        public IActionResult GestionarSolicitudesUsuarioInfluencer(GestionarUsuarioInfluencerViewModel gestionarUsuarioInfluencerViewModel)
        {
            return View();
        }
    }
}

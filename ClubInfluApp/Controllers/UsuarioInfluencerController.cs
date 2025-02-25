using ClubInfluApp.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class UsuarioInfluencerController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioEmpresaService _usuarioEmpresaService;

        public UsuarioInfluencerController(ILogger<HomeController> logger, IUsuarioEmpresaService usuarioEmpresaService)
        {
            _logger = logger;
            _usuarioEmpresaService = usuarioEmpresaService;
        }

        //TODO: Implementar GET ObtenerUsuariosInfluencer, recordar pasar como parametro a la vista, la lista de usuarios influencer que optienen en el servicio.
    }
}

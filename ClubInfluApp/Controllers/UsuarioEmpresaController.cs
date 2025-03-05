using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class UsuarioEmpresaController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly IUsuarioEmpresaService _usuarioEmpresaService;

        public UsuarioEmpresaController(ILogger<InicioController> logger, IUsuarioEmpresaService usuarioEmpresaService)
        {
            _logger = logger;
            _usuarioEmpresaService = usuarioEmpresaService;
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
            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuarioEmpresa(NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(nuevoUsuarioEmpresaViewModel);
            }

            int idUsuarioEmpresa = _usuarioEmpresaService.CrearUsuarioEmpresa(nuevoUsuarioEmpresaViewModel);

            ViewBag.Mensaje = "Registro completado. Revisaremos tu información y nos pondremos en contacto pronto.El equipo de Club Influ";
            return View();
        }
    }
}

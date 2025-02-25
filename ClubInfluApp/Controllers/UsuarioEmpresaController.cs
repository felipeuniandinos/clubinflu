using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace ClubInfluApp.Controllers
{
    public class UsuarioEmpresaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioEmpresaService _usuarioEmpresaService;

        public UsuarioEmpresaController(ILogger<HomeController> logger, IUsuarioEmpresaService usuarioEmpresaService)
        {
            _logger = logger;
            _usuarioEmpresaService = usuarioEmpresaService;
        }

        //TODO: Implementar GET ObtenerUsuariosEmpresa, recordar pasar como parametro a la vista, la lista de usuarios empresa que optienen en el servicio.

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

            ViewBag.Mensaje = "Usuario creado exitosamente. Con el id:" + idUsuarioEmpresa;
            return View();
        }
    }
}

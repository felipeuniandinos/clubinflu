using System.Reflection;
using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.BusinessLogic.Services;
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

        public UsuarioEmpresaController(ILogger<InicioController> logger, IUsuarioEmpresaService usuarioEmpresaService, IPaisService paisService)
        {
            _logger = logger;
            _usuarioEmpresaService = usuarioEmpresaService;
            _paisService = paisService;
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
            return View(nuevoUsuarioEmpresaViewModel);
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

        [HttpGet]
        public IActionResult GestionarUsuarioEmpresa(int idUsuarioEmpresa)
        {
            DetalleUsuarioEmpresaViewModel detalleUsuarioEmpresa = _usuarioEmpresaService.ObtenerDetalleUsuarioEmpresa(idUsuarioEmpresa);
            return View(detalleUsuarioEmpresa);
        }

        [HttpPut]
        public IActionResult ModificarEstadoUsuarioEmpresa(int idUsuarioEmpresa, int idNuevoEstadoUsuario)
        {
            _usuarioEmpresaService.ModificacionEstadoUsuarioEmpresa(idUsuarioEmpresa, idNuevoEstadoUsuario);
            return Json(new { mensaje = "El usuario empresa esta actualizado con exito" });
        }
    }
}

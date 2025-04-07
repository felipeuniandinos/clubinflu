using System.Reflection;
using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class UsuarioEmpresaController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioEmpresaService _usuarioEmpresaService;

        public UsuarioEmpresaController(
            ILogger<HomeController> logger,
            IUsuarioEmpresaService usuarioEmpresaService
        )
        {
            _logger = logger;
            _usuarioEmpresaService = usuarioEmpresaService;
        }

        [HttpGet]
        public IActionResult ListarUsuariosEmpresa()
        {
            try
            {
                List<UsuarioEmpresaViewModel> usuariosEmpresa =
                    _usuarioEmpresaService.ObtenerUsuariosEmpresa();

                return View(usuariosEmpresa);
            }
            catch (Exception e)
            {
                _logger.LogError($"Error al obtener listado de Usuarios Empresa: ", (e.Message));
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult CrearUsuarioEmpresa()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CrearUsuarioEmpresa(
            NuevoUsuarioEmpresaViewModel nuevoUsuarioEmpresaViewModel
        )
        {
            if (!ModelState.IsValid)
            {
                return View(nuevoUsuarioEmpresaViewModel);
            }

            int idUsuarioEmpresa = _usuarioEmpresaService.CrearUsuarioEmpresa(
                nuevoUsuarioEmpresaViewModel
            );

            ViewBag.Mensaje = "Usuario creado exitosamente. Con el id:" + idUsuarioEmpresa;
            return View();
        }

        [HttpGet]
        public IActionResult GestionarUsuarioEmpresa(int idUsuarioEmpresa)
        {
            DetalleUsuarioEmpresaViewModel detalleUsuarioEmpresa =
                _usuarioEmpresaService.ObtenerDetalleUsuarioEmpresa(idUsuarioEmpresa);
            return View(detalleUsuarioEmpresa);
        }

        [HttpPut]
        public IActionResult ModificarEstadoUsuarioEmpresa(
            int idUsuarioEmpresa,
            int idActualEstadoUsuario,
            int idNuevoEstadoUsuario
        )
        {
            _usuarioEmpresaService.ModificacionEstadoUsuarioEmpresa(
                idUsuarioEmpresa,
                idActualEstadoUsuario,
                idNuevoEstadoUsuario
            );
            return View("ListarUsuariosEmpresa");
        }
    }
}

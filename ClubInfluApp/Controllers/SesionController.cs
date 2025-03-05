using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClubInfluApp.Controllers
{
    public class SesionController : Controller
    {
        private readonly ILogger<InicioController> _logger;
        private readonly IUsuarioService _usuarioService;

        public SesionController(ILogger<InicioController> logger, IUsuarioService usuarioService)
        {
            _logger = logger;
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public IActionResult InicioSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> InicioSesion(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(usuarioViewModel);
            }

            int idUsuarioAutenticado = _usuarioService.ObtenerIdUsuario(usuarioViewModel);
            if (idUsuarioAutenticado == 0)
            {
                ViewBag.Mensaje = "Usuario incorrecto";
                return View();
            }

            string tipoUsuario = usuarioViewModel.tipo.ToString();


            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuarioViewModel.correo),
                new Claim(ClaimTypes.Role, tipoUsuario),
                new Claim("UserId", idUsuarioAutenticado.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(claims, "CookieAuth");
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync("CookieAuth", new ClaimsPrincipal(claimsIdentity), authProperties);

            switch (usuarioViewModel.tipo)
            {
                case TipoUsuario.Empresa:
                    return RedirectToAction("InicioSesion", "Sesion");
                case TipoUsuario.Influencer:
                    return RedirectToAction("InicioSesion", "Sesion");
                case TipoUsuario.Administrador:
                    return RedirectToAction("ListarUsuariosInfluencer", "UsuarioInfluencer");
                default:
                    return RedirectToAction("InicioSesion", "Sesion");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync("CookieAuth");
            return RedirectToAction("InicioSesion", "Sesion");
        }

        [HttpGet]
        public IActionResult AccesoDenegado()
        {
            return View();
        }
    }
}

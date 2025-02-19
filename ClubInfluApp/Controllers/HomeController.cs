using System.Diagnostics;
using ClubInfluApp.Data.Interfaces;
using ClubInfluApp.Data.Repositories;
using ClubInfluApp.Models;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsuarioEmpresaRepository _usuarioEmpresaRepository;

        public HomeController(ILogger<HomeController> logger, IUsuarioEmpresaRepository usuarioEmpresaRepository)
        {
            _logger = logger;
            _usuarioEmpresaRepository = usuarioEmpresaRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            //Empresa empresa = new Empresa();
            //empresa.idCiudad = 1;
            //empresa.nombre = "Empresa 1";
            //empresa.url = "www.empresa1.com";
            //empresa.numeroContacto = "123456789";
            //empresa.sector = "Tecnología";
            //empresa.direccion = "Calle 123";
            
            //UsuarioEmpresa usuarioEmpresa = new UsuarioEmpresa();
            //usuarioEmpresa.idEstadoUsuario = 3;
            //usuarioEmpresa.idEmpresa = 0;
            //usuarioEmpresa.correo = "demo@test.com";
            //usuarioEmpresa.clave = "123456";
            //usuarioEmpresa.fechaCreacion = DateTime.Now;
            //usuarioEmpresa.fechaActualizacion = DateTime.Now;

            //_usuarioEmpresaRepository.CrearUsuarioEmpresa(usuarioEmpresa, empresa);


            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

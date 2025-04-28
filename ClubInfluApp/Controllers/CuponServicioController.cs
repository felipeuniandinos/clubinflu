using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class CuponServicioController : Controller
    {
        [Authorize(Roles = "Influencer")]
        public IActionResult ListarCuponesDeServicioOfertados()
        {
            return View();
        }

        [Authorize(Roles = "Empresa")]
        public IActionResult HistoricoCuponesDeServicio()
        {
            return View();
        }
    }
}

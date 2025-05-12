using System.Diagnostics;
using ClubInfluApp.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class InicioController : Controller
    {
        private readonly ILogger<InicioController> _logger;

        public InicioController(ILogger<InicioController> logger)
        {
            _logger = logger;
        }

        public IActionResult Inicio()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            Exception exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            if (!string.IsNullOrEmpty(exception.Message) && exception.Message.Contains("|BL|:")) 
            {
                string referer = HttpContext.Request.Headers["Referer"].ToString();

                if (!string.IsNullOrEmpty(referer))
                {
                    TempData["ExceptionBL"] = exception.Message.Replace("|BL|:", "");
                    return Redirect(referer);
                }
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

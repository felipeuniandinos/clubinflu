using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    [Authorize]
    public class OfferController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

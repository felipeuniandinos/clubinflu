using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClubInfluApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService) 
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            List<User> users = _userService.GetAllUsers();
            return View(users);
        }
    }
}

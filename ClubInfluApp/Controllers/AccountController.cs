using ClubInfluApp.BusinessLogic.Interfaces;
using ClubInfluApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ClubInfluApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Login()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!authenticateResult.Succeeded)
                return RedirectToAction("Login");

            var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;
            var email = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var name = claims?.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

            var user = await _userService.GetUserByEmailAsync(email);
            if (user == null)
            {
                TempData["Email"] = email;
                TempData["Name"] = name;
                return RedirectToAction("SelectRole");
            }

            await SignInUser(user);
            return RedirectToAction("Index", "Offer");
        }

        public IActionResult SelectRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SelectRole(string role)
        {
            var email = TempData["Email"] as string;
            var name = TempData["Name"] as string;

            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(name))
                return RedirectToAction("Login");

            var created = await _userService.CreateUserAsync(email, name, role);
            if (!created)
            {
                // Manejo de error si el usuario ya existe
                return RedirectToAction("Login");
            }

            var user = await _userService.GetUserByEmailAsync(email);
            await SignInUser(user);
            return RedirectToAction("Index", "Offer");
        }

        private async Task SignInUser(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties { IsPersistent = true };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> AccessDenied()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}

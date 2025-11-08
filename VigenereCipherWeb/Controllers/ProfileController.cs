using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VigenereCipherWeb.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            var claims = User.Claims.ToDictionary(c => c.Type, c => c.Value);

            ViewBag.Nickname = User.FindFirst("nickname")?.Value;
             ViewBag.Email =
                User.FindFirst("email")?.Value
                ?? User.FindFirst(ClaimTypes.Email)?.Value
                ?? User.Claims.FirstOrDefault(c => c.Type?.EndsWith("/email") == true)?.Value
                ?? User.Claims.FirstOrDefault(c => c.Type?.ToLower().Contains("email") == true)?.Value;
            ViewBag.Name = User.FindFirst("name")?.Value;
            ViewBag.Picture = User.FindFirst("picture")?.Value;

            return View("ProfilePage", claims);
        }
    }
}

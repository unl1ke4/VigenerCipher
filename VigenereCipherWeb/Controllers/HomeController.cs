using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace VigenereCipherWeb.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            var nickname = User.FindFirst("nickname")?.Value;
            var name = User.Identity?.Name;
            var email = User.FindFirst("email")?.Value;

            ViewBag.UserName = nickname ?? name ?? email ?? "Невідомий користувач";
            return View();
        }
    }
}

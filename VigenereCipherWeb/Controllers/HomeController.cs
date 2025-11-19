using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VigenereCipherWeb.Data;

namespace VigenereCipherWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            // беремо ID з NameIdentifier
            var auth0Id =
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                ?? User.FindFirst("sub")?.Value; // на всякий випадок

            if (string.IsNullOrEmpty(auth0Id))
            {
                // якщо навіть тут нічого немає — не ліземо в БД
                var name = User.Identity?.Name;
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                ViewBag.UserName = name ?? email ?? "Unknown user (no id)";
                return View("HomePage");
            }

            var user = await _dbContext.AppUsers
                .FirstOrDefaultAsync(u => u.Auth0UserId == auth0Id);

            if (user == null)
            {
                user = new Data.AppUser
                {
                    Auth0UserId = auth0Id,
                    Username = User.FindFirst("nickname")?.Value
                               ?? User.Identity?.Name
                               ?? "Unknown",
                    Email = User.FindFirst(ClaimTypes.Email)?.Value ?? ""
                };

                _dbContext.AppUsers.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            ViewBag.UserName = user.Username;
            return View("HomePage");
        }
    }
}

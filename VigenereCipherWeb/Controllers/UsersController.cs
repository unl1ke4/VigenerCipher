using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VigenereCipherWeb.Data;

namespace VigenereCipherWeb.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.AppUsers
                .Include(u => u.CipherJobs)
                .OrderBy(u => u.Username)
                .ToListAsync();

            return View(users);
        }

        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.AppUsers
                .Include(u => u.CipherJobs)
                    .ThenInclude(j => j.CipherMethod)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
    }
}

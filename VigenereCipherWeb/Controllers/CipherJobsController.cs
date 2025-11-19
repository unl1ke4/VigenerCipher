using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VigenereCipherWeb.Data;
using VigenereCipherWeb.Models;

namespace VigenereCipherWeb.Controllers
{
    [Authorize]
    public class CipherJobsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CipherJobsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var auth0Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
            var currentUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.Auth0UserId == auth0Id);
            
            if (currentUser == null)
            {
                ViewBag.Message = $"Користувача з Auth0UserId '{auth0Id}' не знайдено в БД. Показуємо всі завдання.";
                var allJobs = await _context.CipherJobs
                    .Include(j => j.AppUser)
                    .Include(j => j.CipherMethod)
                    .OrderByDescending(j => j.CreatedAt)
                    .ToListAsync();
                return View(allJobs);
            }

            var jobs = await _context.CipherJobs
                .Where(j => j.AppUserId == currentUser.Id)
                .Include(j => j.AppUser)
                .Include(j => j.CipherMethod)
                .OrderByDescending(j => j.CreatedAt)
                .ToListAsync();

            ViewBag.Message = $"Користувач: {currentUser.Username} (ID: {currentUser.Id}, Auth0: {currentUser.Auth0UserId}). Знайдено завдань: {jobs.Count}";

            return View(jobs);
        }

        public async Task<IActionResult> Details(int id)
        {
            var auth0Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
            var currentUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.Auth0UserId == auth0Id);
            
            if (currentUser == null)
            {
                return NotFound("User not found in database");
            }

            var job = await _context.CipherJobs
                .Include(j => j.AppUser)
                .Include(j => j.CipherMethod)
                .FirstOrDefaultAsync(j => j.Id == id && j.AppUserId == currentUser.Id);

            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        [HttpGet]
        public IActionResult Search()
        {
            var model = new SearchViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            var auth0Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
            var currentUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.Auth0UserId == auth0Id);
            
            if (currentUser == null)
            {
                return NotFound("User not found in database");
            }

            var query = _context.CipherJobs
                .Where(j => j.AppUserId == currentUser.Id)
                .Include(j => j.AppUser)
                .Include(j => j.CipherMethod)
                .AsQueryable();

            if (model.DateFrom.HasValue)
            {
                query = query.Where(j => j.CreatedAt >= model.DateFrom.Value);
            }

            if (model.DateTo.HasValue)
            {
                query = query.Where(j => j.CreatedAt <= model.DateTo.Value);
            }

            if (!string.IsNullOrWhiteSpace(model.InputTextStartsWith))
            {
                query = query.Where(j => j.InputText.StartsWith(model.InputTextStartsWith));
            }

            if (!string.IsNullOrWhiteSpace(model.InputTextEndsWith))
            {
                query = query.Where(j => j.InputText.EndsWith(model.InputTextEndsWith));
            }

            if (!string.IsNullOrWhiteSpace(model.ResultTextStartsWith))
            {
                query = query.Where(j => j.ResultText.StartsWith(model.ResultTextStartsWith));
            }

            if (!string.IsNullOrWhiteSpace(model.ResultTextEndsWith))
            {
                query = query.Where(j => j.ResultText.EndsWith(model.ResultTextEndsWith));
            }

            var results = await query
                .OrderByDescending(j => j.CreatedAt)
                .Select(j => new CipherJobSearchResult
                {
                    Id = j.Id,
                    InputText = j.InputText,
                    Key = j.Key,
                    Operation = j.Operation,
                    ResultText = j.ResultText,
                    CreatedAt = j.CreatedAt,
                    Username = j.AppUser != null ? j.AppUser.Username : "N/A",
                    UserEmail = j.AppUser != null ? j.AppUser.Email : "N/A",
                    MethodName = j.CipherMethod.Name,
                    MethodDescription = j.CipherMethod.Description
                })
                .ToListAsync();

            model.Results = results;

            return View(model);
        }
    }
}

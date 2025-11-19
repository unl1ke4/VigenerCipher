using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VigenereCipherWeb.Data;

namespace VigenereCipherWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AppUsersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AppUsersApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Data.AppUser>>> GetAppUsers()
        {
            return await _context.AppUsers.ToListAsync();
        }
    }
}
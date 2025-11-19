using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VigenereCipherWeb.Data;

namespace VigenereCipherWeb.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CipherMethodsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CipherMethodsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CipherMethodsApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CipherMethod>>> GetCipherMethods()
        {
            return await _context.CipherMethods.ToListAsync();
        }
    }
}
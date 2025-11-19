using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VigenereCipherWeb.Data;
using VigenereCipherWeb.Models;
using ApiVersion = Microsoft.AspNetCore.Mvc.ApiVersion;

namespace VigenereCipherWeb.Controllers.Api
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CipherJobsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CipherJobsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ApiVersion("1.0")]
        public async Task<ActionResult<IEnumerable<Data.CipherJob>>> GetCipherJobsV1()
        {
            return await _context.CipherJobs.AsNoTracking().ToListAsync();
        }

        [HttpGet]
        [ApiVersion("2.0")]
        public async Task<ActionResult<IEnumerable<CipherJobV2Dto>>> GetCipherJobsV2()
        {
            var jobs = await _context.CipherJobs
                .Include(j => j.AppUser)
                .Include(j => j.CipherMethod)
                .Select(j => new CipherJobV2Dto
                {
                    Id = j.Id,
                    InputText = j.InputText,
                    ResultText = j.ResultText,
                    Operation = j.Operation,
                    CreatedAt = j.CreatedAt,
                    UserName = j.AppUser != null ? j.AppUser.Username : "Unknown",
                    CipherMethodName = j.CipherMethod.Name
                })
                .AsNoTracking()
                .ToListAsync();

            return Ok(jobs);
        }
    }
}
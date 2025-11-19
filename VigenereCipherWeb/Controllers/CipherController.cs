using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using VigenereCipherLib; 
using VigenereCipherWeb.Data;
using VigenereCipherWeb.Models; 
using Microsoft.AspNetCore.Authorization; 

namespace VigenereCipherWeb.Controllers
{
    [Authorize] 
    public class CipherController : Controller
    {
        private readonly VigenereCipherModel _cipherModel;
        private readonly ApplicationDbContext _context;

        public CipherController(ApplicationDbContext context)
        {
            _cipherModel = new VigenereCipherModel();
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("CipherPage", new VigenereViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Index(VigenereViewModel model)
        {
            if (string.IsNullOrEmpty(model.InputText) || string.IsNullOrEmpty(model.Key))
            {
                ViewBag.Error = "The text and key cannot be empty!";
                return View("CipherPage", model);
            }

            try
            {
                if (model.Operation == "encrypt")
                {
                    model.ResultText = _cipherModel.Encrypt(model.InputText, model.Key);
                }
                else
                {
                    model.ResultText = _cipherModel.Decrypt(model.InputText, model.Key);
                }

                await SaveCipherJobAsync(model);
                
                ViewBag.Success = "Операцію виконано та збережено в історію!";
            }
            catch (Exception ex)
            {
                model.ResultText = $"Error: {ex.Message}";
            }

            return View("CipherPage", model);
        }

        private async Task SaveCipherJobAsync(VigenereViewModel model)
        {
            var auth0Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? User.FindFirst("sub")?.Value;
            var currentUser = await _context.AppUsers.FirstOrDefaultAsync(u => u.Auth0UserId == auth0Id);
            
            if (currentUser == null)
            {
                var username = User.FindFirst(ClaimTypes.Name)?.Value ?? 
                               User.FindFirst("name")?.Value ?? 
                               "Unknown";
                var email = User.FindFirst(ClaimTypes.Email)?.Value ?? 
                           User.FindFirst("email")?.Value ?? 
                           "unknown@example.com";

                currentUser = new Data.AppUser
                {
                    Auth0UserId = auth0Id ?? "unknown",
                    Username = username,
                    Email = email
                };

                _context.AppUsers.Add(currentUser);
                await _context.SaveChangesAsync();
            }

            var vigenereMethod = await _context.CipherMethods.FirstOrDefaultAsync(m => m.Name == "Vigenere");
            
            if (vigenereMethod == null)
            {
                vigenereMethod = new Data.CipherMethod
                {
                    Name = "Vigenere",
                    Description = "Класичний шифр Віженера"
                };
                _context.CipherMethods.Add(vigenereMethod);
                await _context.SaveChangesAsync();
            }

            var cipherJob = new Data.CipherJob
            {
                InputText = model.InputText!,
                Key = model.Key!,
                Operation = model.Operation,
                ResultText = model.ResultText ?? "",
                CreatedAt = DateTime.UtcNow,
                AppUserId = currentUser.Id,
                CipherMethodId = vigenereMethod.Id
            };

            _context.CipherJobs.Add(cipherJob);
            await _context.SaveChangesAsync();
        }
    }
}
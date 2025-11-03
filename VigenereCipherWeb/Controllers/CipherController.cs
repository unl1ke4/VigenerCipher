using Microsoft.AspNetCore.Mvc;
using VigenereCipherLib; 
using VigenereCipherWeb.Models; 
using Microsoft.AspNetCore.Authorization; 

namespace VigenereCipherWeb.Controllers
{
    [Authorize] 
    public class CipherController : Controller
    {
        private readonly VigenereCipherModel _cipherModel;

        public CipherController()
        {
            _cipherModel = new VigenereCipherModel(); 
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new VigenereViewModel());
        }

        [HttpPost]
        public IActionResult Index(VigenereViewModel model)
        {
            if (string.IsNullOrEmpty(model.InputText) || string.IsNullOrEmpty(model.Key))
            {
                ViewBag.Error = "Текст і ключ не можуть бути порожніми.";
                return View(model);
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
            }
            catch (Exception ex)
            {
                model.ResultText = $"Помилка: {ex.Message}";
            }

            return View(model);
        }
    }
}
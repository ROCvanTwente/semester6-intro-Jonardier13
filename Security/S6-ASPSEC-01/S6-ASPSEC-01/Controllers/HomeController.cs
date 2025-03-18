using System.Diagnostics;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using S6_ASPSEC_01.Models;

namespace S6_ASPSEC_01.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Versleutelen()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Versleutelen(string textToEncrypt, int key)
        {
            if (string.IsNullOrEmpty(textToEncrypt))
            {
                ViewBag.Message = "Vul zowel de tekst als de sleutel in!";
                return View();
            }

            string encryptedText = EncryptionFunctions.Encrypt(textToEncrypt, key);
            ViewBag.EncryptedText = encryptedText;
            ViewBag.OriginalText = textToEncrypt;
            ViewBag.Key = key;

            return View();
        }
        public IActionResult onsleutelen()
        {
            return View();
        }
        [HttpPost]
        public IActionResult onsleutelen(string textTodecrypt, int key)
        {
            if (string.IsNullOrEmpty(textTodecrypt))
            {
                ViewBag.Message = "Vul zowel de tekst als de sleutel in!";
                return View();
            }

            string decryptedText = EncryptionFunctions.Decrypt(textTodecrypt, key);
            ViewBag.DecryptedText = decryptedText;
            ViewBag.OriginalText = textTodecrypt;
            ViewBag.Key = key;

            return View();

        }











        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

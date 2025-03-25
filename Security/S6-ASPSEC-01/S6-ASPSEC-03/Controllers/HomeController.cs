using Microsoft.AspNetCore.Mvc;
using S6_ASPSEC_03.Models;
using System.Diagnostics;

namespace S6_ASPSEC_03.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string textToHash, string algorithm)
        {
            if (string.IsNullOrEmpty(textToHash) || string.IsNullOrEmpty(algorithm))
            {
                ViewBag.Message = "Voer een tekst in en kies een algoritme!";
                return View();
            }

            string hashedText = HashFunctions.HashText(textToHash, algorithm);
            ViewBag.OriginalText = textToHash;
            ViewBag.Algorithm = algorithm;
            ViewBag.HashedText = hashedText;

            return View();
        }








            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

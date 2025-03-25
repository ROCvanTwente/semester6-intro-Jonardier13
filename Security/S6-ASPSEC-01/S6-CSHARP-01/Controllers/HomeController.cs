using Microsoft.AspNetCore.Mvc;
using S6_CSHARP_01.Models;
using System.Diagnostics;

namespace S6_CSHARP_01.Controllers
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

        public IActionResult FirstCharUp()
        {
            return View();
        }
        [HttpPost]
        public IActionResult FirstCharUp(string tekst)
        {
            // Sla de originele tekst op
            ViewBag.InputString = tekst;

            // Roep de Extension Method aan om de tekst om te zetten
            ViewBag.Output = ExtentionMethods.FirstCharToUpper(tekst);

            // Retourneer de view
            return View();
        }


        public IActionResult AddMin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddMin(DateTime inputDate, int minutes)
        {
            // Controleer of de ingevoerde datum geldig is
            if (inputDate == default(DateTime))
            {
                ViewBag.Output = "Ongeldige datum ingevoerd.";
                return View();
            }

            // Roep de AddMinutes extensiemethode aan
            var newDate = inputDate.AddMinutes(minutes);

            // Gegevens doorgeven aan de view
            ViewBag.OriginalDate = inputDate.ToString("yyyy-MM-dd HH:mm:ss");
            ViewBag.NewDate = newDate.ToString("yyyy-MM-dd HH:mm:ss");

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using S6_CSHARP_02.Models;
using System.Diagnostics;

namespace S6_CSHARP_02.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Vehicle()
        {

            return View();
        }
        // Creëer een voertuig op basis van de keuze van de gebruiker
        [HttpPost]
        public IActionResult Vehicle(string vehicleType)
        {
            VehicleService.CreateVehicle(vehicleType);  // Maak het voertuig aan
            var vehicle = VehicleService.GetVehicle();  // Haal het voertuig op uit de service
            return View("Details", vehicle);  // Toon de details van het voertuig
        }

        // Verhoog de snelheid van het voertuig
        [HttpPost]
        public IActionResult IncreaseSpeed()
        {
            VehicleService.IncreaseSpeed(10);  // Verhoog de snelheid met 10
            var vehicle = VehicleService.GetVehicle();  // Haal het voertuig op uit de service
            return View("Details", vehicle);  // Toon de details van het voertuig
        }

        // Verlaag de snelheid van het voertuig
        [HttpPost]
        public IActionResult DecreaseSpeed()
        {
            VehicleService.DecreaseSpeed(10);  // Verlaag de snelheid met 10
            var vehicle = VehicleService.GetVehicle();  // Haal het voertuig op uit de service
            return View("Details", vehicle);  // Toon de details van het voertuig
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

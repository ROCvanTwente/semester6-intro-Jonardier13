using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace S6_ASPSEC_04.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // Deze actie toont het profiel van de ingelogde gebruiker
        public IActionResult Profile()
        {
            var userId = User.Identity.Name;  // Haal de naam van de ingelogde gebruiker op
                                              // Logica om alleen toegang te geven aan de ingelogde gebruiker zelf
            if (userId == User.Identity.Name)
            {
                return View();  // Toon de profielpagina van de gebruiker
            }

            return Unauthorized();  // Gebruiker is niet geautoriseerd om deze pagina te zien
        }

        [Authorize(Roles = "I0SD1")]
        public IActionResult I0SD1Page()
        {
            return View();
        }

        [Authorize(Roles = "I0SD3")]
        public IActionResult I0SD3Page()
        {
            return View();
        }

    }
}

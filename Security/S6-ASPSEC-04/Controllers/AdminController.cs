using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using S6_ASPSEC_04.Models;

namespace S6_ASPSEC_04.Controllers
{


    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AdminController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        // Haalt de pagina op waar alle gebruikers worden weergegeven
        public async Task<IActionResult> AdminIndex()
        {
            // Haalt alle gebruikers op uit de database via UserManager
            var users = _userManager.Users.ToList();

            // Deze lijst houdt de gegevens bij die we naar de View willen sturen
            var userViewModels = new List<UserViewModel>();

            // Voor elke gebruiker halen we de rollen op en stoppen we het in het ViewModel
            foreach (var user in users)
            {
                // Haalt de rollen op die aan deze gebruiker gekoppeld zijn
                var roles = await _userManager.GetRolesAsync(user);

                // Maakt een nieuw ViewModel aan met de gebruikersgegevens
                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,           // Gebruikers-ID
                    Email = user.Email,     // Gebruikers-e-mail
                    Roles = string.Join(", ", roles) // Maakt van de lijst een enkele string
                });
            }

            // Stuurt de lijst van gebruikers met hun rollen naar de View
            return View(userViewModels);
        }


    }
}

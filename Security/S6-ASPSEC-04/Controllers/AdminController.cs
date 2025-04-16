using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S6_ASPSEC_04.Models;


namespace S6_ASPSEC_04.Controllers
{


    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;



        public AdminController(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }



        public IActionResult CreateUser()
        {
            return View();
        }
        // Verwerk het formulier om een gebruiker toe te voegen
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Rol toevoegen aan gebruiker
                    await _userManager.AddToRoleAsync(user, model.Role);
                    TempData["SuccessMessage"] = "De gebruiker is succesvol toegevoegd!";
                    return RedirectToAction("AdminIndex");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // Haalt de pagina op waar alle gebruikers worden weergegeven
        public async Task<IActionResult> AdminIndex()
        {
            var users = _userManager.Users.ToList(); // Haal alle gebruikers op
            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync(); // Haal alle rollen op uit de RoleManager

            var userViewModels = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user); // Haal de rollen van de gebruiker op

                userViewModels.Add(new UserViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    Roles = string.Join(", ", roles), // Voegt alle rollen samen in een string
                    SelectedRole = roles.FirstOrDefault(), // De huidige rol van de gebruiker
                    AllRoles = allRoles // Geeft alle beschikbare rollen door
                });
            }

            return View(userViewModels); // Stuur de gebruikersgegevens en rollen naar de view
        }



        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    // Gebruiker is succesvol verwijderd
                    TempData["SuccessMessage"] = "Gebruiker is succesvol verwijderd.";
                }
                else
                {
                    // Fout bij het verwijderen van de gebruiker
                    TempData["ErrorMessage"] = "Er is een fout opgetreden bij het verwijderen van de gebruiker.";
                }
            }
            return RedirectToAction("AdminIndex");
        }



        [HttpPost]
        public async Task<IActionResult> ChangeUserRole(string userId, string roleName)
        {
            // Haal de gebruiker op uit de database
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "De gebruiker kon niet worden gevonden.";
                return RedirectToAction("AdminIndex");
            }

            // Haal de lijst van beschikbare rollen uit de database via RoleManager
            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();
            if (!allRoles.Contains(roleName))
            {
                TempData["ErrorMessage"] = "De opgegeven rol is ongeldig.";
                return RedirectToAction("AdminIndex");
            }

            // Zorg ervoor dat de admin zijn eigen rol niet per ongeluk wijzigt
            if (User.IsInRole("Admin") && user.UserName == User.Identity.Name && roleName != "Admin")
            {
                TempData["ErrorMessage"] = "Je kunt je eigen rol niet verwijderen of wijzigen.";
                return RedirectToAction("AdminIndex");
            }

            // Haal de huidige rollen van de gebruiker op
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Verwijder de gebruiker uit alle bestaande rollen
            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                TempData["ErrorMessage"] = "Er is een fout opgetreden bij het verwijderen van de oude rollen.";
                return RedirectToAction("AdminIndex");
            }

            // Voeg de gebruiker toe aan de nieuwe rol
            result = await _userManager.AddToRoleAsync(user, roleName);
            if (result.Succeeded)
            {
                TempData["SuccessMessage"] = $"De rol van gebruiker {user.UserName} is succesvol gewijzigd naar {roleName}.";
            }
            else
            {
                TempData["ErrorMessage"] = "Er is een fout opgetreden bij het toewijzen van de nieuwe rol.";
            }

            // Redirect terug naar de AdminIndex
            return RedirectToAction("AdminIndex");
        }




    }
}

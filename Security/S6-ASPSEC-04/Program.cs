using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using S6_ASPSEC_04.Data;

namespace S6_ASPSEC_04
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Voeg de services toe aan de container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // Gebruik AddIdentity in plaats van AddDefaultIdentity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultUI() // Voeg standaard UI toe voor login en registratie
                .AddDefaultTokenProviders(); // Voeg de token providers toe voor extra functionaliteit

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Zorg ervoor dat je de rollen aanmaakt voordat de app draait
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var userManager = services.GetRequiredService<UserManager<IdentityUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await CreateRolesAsync(services); //  Alleen deze blijft staan
                await CreateAdminUser(services, userManager); // ?? Deze mag erbij
            }



            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication(); // Deze is vaak vergeten
            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            app.MapRazorPages()
                .WithStaticAssets();

            app.Run();
        }

        // De 'CreateRolesAsync' blijft hetzelfde
        static async Task CreateRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roleNames = { "Admin", "Gebruiker", "I0SD1", "I0SD3" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task CreateAdminUser(IServiceProvider serviceProvider, UserManager<IdentityUser> userManager)
        {
            var adminEmail = "admin2@admin.com";  // Je kunt dit e-mailadres aanpassen
            var adminPassword = "Admin123@!";  // Gebruik een sterk wachtwoord

            var user = await userManager.FindByEmailAsync(adminEmail);

            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                var result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");  // Voeg de admin-rol toe aan de gebruiker
                }
            }
        }
    }
}
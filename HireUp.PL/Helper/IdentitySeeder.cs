using HireUp.DAL.Models;
using HireUp.DAL.Models.Auth;
using Microsoft.AspNetCore.Identity;

namespace HireUp.PL.Helper
{
    public static class IdentitySeeder
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles = { "Admin", "Company", "JobSeeker" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                    await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
        public static async Task SeedAdmin(UserManager<ApplicationUser> userManager)
        {
            string adminEmail = "elkarim.online@gmail.com";
            string adminPassword = "Admin@123";

            var user = await userManager.FindByEmailAsync(adminEmail);
            if (user == null)
            {
                user = new Admin
                {
                    UserName = adminEmail.Split('@')[0],
                    Email = adminEmail,
                    EmailConfirmed = true,
                    Country = "Egypt",
                    Location = "Giza",
                };
                await userManager.CreateAsync(user, adminPassword);
            }

            if (!await userManager.IsInRoleAsync(user, "Admin"))
            {
                await userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }

}

using Microsoft.AspNetCore.Identity;
using Nicopolis_Ad_Istrum.Models.Identity;

namespace Nicopolis_Ad_Istrum.Data.SeedDb
{
    public class SeedData
    {
        public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Administrator", "Science Associate", "Employee", "User" };

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        public static async Task SeedUsersAsync(UserManager<ApplicationUser> userManager)
        {
            var users = new List<(string Email, string Password, string Role)>
            {
                ("admin@domain.com", "AdminPassword123!", "Administrator"),
                ("associate@domain.com", "AssociatePassword123!", "Science Associate"),
                ("employee@domain.com", "EmployeePassword123!", "Employee"),
                ("user@domain.com", "UserPassword123!", "User")
            };

            foreach (var (email, password, role) in users)
            {
                var user = await userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    user = new ApplicationUser
                    {
                        UserName = email,
                        Email = email
                    };

                    var result = await userManager.CreateAsync(user, password);

                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, role);
                    }
                }
            }
        }

        public static async Task SeedDataAsync(IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            await SeedRolesAsync(roleManager);

            await SeedUsersAsync(userManager);
        }
    }
}

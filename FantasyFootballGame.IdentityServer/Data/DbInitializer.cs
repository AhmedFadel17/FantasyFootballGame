using FantasyFootballGame.IdentityServer.Enums;
using Microsoft.AspNetCore.Identity;

namespace FantasyFootballGame.IdentityServer.Data
{
    public static class DbInitializer
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roleNames = Enum.GetNames(typeof(UserRole));

            foreach (var roleName in roleNames)
            {
                if (!await roleManager.RoleExistsAsync(roleName))
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}

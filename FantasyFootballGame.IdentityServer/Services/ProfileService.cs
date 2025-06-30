using Duende.IdentityModel;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using FantasyFootballGame.IdentityServer.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FantasyFootballGame.IdentityServer.Services
{
    public sealed class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<IdentityUser> _claimsFactory;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ProfileService(
            IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _claimsFactory = claimsFactory;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
            if (user == null)
                throw new ArgumentException("User not found");

            ClaimsPrincipal userClaims = await _claimsFactory.CreateAsync(user);

            var claims = userClaims.Claims
                .Where(claim => context.RequestedClaimTypes.Contains(claim.Type))
                .ToList();

            // Add userId, username, and email manually
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName ?? ""));
            claims.Add(new Claim(ClaimTypes.Email, user.Email ?? ""));

            // Add role claims
            if (_userManager.SupportsUserRole)
            {
                var roles = await _userManager.GetRolesAsync(user);
                foreach (var roleName in roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, roleName));
                    if (_roleManager.SupportsRoleClaims)
                    {
                        var role = await _roleManager.FindByNameAsync(roleName);
                        if (role != null)
                        {
                            claims.AddRange(await _roleManager.GetClaimsAsync(role));
                        }
                    }
                }
            }

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await _userManager.FindByIdAsync(context.Subject.GetSubjectId());
            context.IsActive = user != null;
        }
    }
}

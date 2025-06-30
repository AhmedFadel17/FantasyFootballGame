using System.Security.Claims;

namespace FantasyFootballGame.API.Extensions
{
    public static class UserClaimsExtensions
    {
        public static string? GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }
}

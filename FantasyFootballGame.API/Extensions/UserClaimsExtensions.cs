using System.Security.Claims;

namespace FantasyFootballGame.API.Extensions
{
    public static class UserClaimsExtensions
    {
        public static int? GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out var userId))
                return userId;

            return null;
        }
    }
}

using FantasyFootballGame.API.Extensions;
using FantasyFootballGame.API.Factories;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected async Task<IActionResult> HandleUserIdAsync(Func<Guid, Task<IActionResult>> onSuccess)
        {
            var userId = User.GetUserId();
            if (string.IsNullOrWhiteSpace(userId) || !Guid.TryParse(userId, out var guid))
                return Unauthorized(ApiResponseFactory.Error("Invalid user ID."));

            return await onSuccess(guid);
        }

    }
}



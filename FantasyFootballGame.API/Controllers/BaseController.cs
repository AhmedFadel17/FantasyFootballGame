using FantasyFootballGame.API.Extensions;
using FantasyFootballGame.API.Factories;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        // Sync version
        protected IActionResult HandleUserId(Func<int, IActionResult> onSuccess)
        {
            var userId = User.GetUserId();
            if (userId == null)
                return Unauthorized(ApiResponseFactory.Error("User not found."));

            return onSuccess(userId.Value);
        }

        // Async version
        protected async Task<IActionResult> HandleUserIdAsync(Func<int, Task<IActionResult>> onSuccess)
        {
            var userId = User.GetUserId();
            if (userId == null)
                return Unauthorized(ApiResponseFactory.Error("User not found."));

            return await onSuccess(userId.Value);
        }
    }
}



using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.Interfaces.Players;
using FantasyFootballGame.Application.Interfaces.PlayersStats;
using FantasyFootballGame.Domain.Enums.Players;
using FantasyFootballGame.Domain.Enums.User;
using FantasyFootballGame.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Authorize(Roles = $"{nameof(UserRole.Player)}, {nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersStatsController : ControllerBase
    {
        private readonly IPlayersStatsService _service;
        public PlayersStatsController(IPlayersStatsService service)
        {
            _service = service;
        }
        [HttpGet]
        [HttpGet("top/goals")]
        public async Task<IActionResult> GetTopGoalScorers([FromQuery] int? limit=10)
        {
            var players = await _service.GetTopGoalScorers(limit??10);
            return Ok(ApiResponseFactory.Success(players));
        }

        
    }
}

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
        [HttpGet("top/assists")]
        public async Task<IActionResult> GetTopAssists([FromQuery] int? limit = 10)
        {
            var players = await _service.GetTopAssists(limit ?? 10);
            return Ok(ApiResponseFactory.Success(players));
        }

        [HttpGet("top/cleansheets")]
        public async Task<IActionResult> GetTopCleanSheets([FromQuery] int? limit = 10)
        {
            var players = await _service.GetTopCleanSheets(limit ?? 10);
            return Ok(ApiResponseFactory.Success(players));
        }

        [HttpGet("top/minutes")]
        public async Task<IActionResult> GetTopMinutesPlayed([FromQuery] int? limit = 10)
        {
            var players = await _service.GetTopMinutesPlayed(limit ?? 10);
            return Ok(ApiResponseFactory.Success(players));
        }

        [HttpGet("top/saves")]
        public async Task<IActionResult> GetTopSaves([FromQuery] int? limit = 10)
        {
            var players = await _service.GetTopSaves(limit ?? 10);
            return Ok(ApiResponseFactory.Success(players));
        }

        [HttpGet("top/totalpoints")]
        public async Task<IActionResult> GetTopTotalPoints([FromQuery] int? limit = 10)
        {
            var players = await _service.GetTopTotalPoints(limit ?? 10);
            return Ok(ApiResponseFactory.Success(players));
        }

    }
}

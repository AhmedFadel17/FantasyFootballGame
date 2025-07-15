using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.DTOs.GameweekTeams;
using FantasyFootballGame.Application.Interfaces.GameweekTeams;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameweekTeamsController : BaseController
    {
        private readonly IGameweekTeamsService _service;
        public GameweekTeamsController(IGameweekTeamsService service)
        {
            _service = service;
        }

        [Authorize(Roles = nameof(UserRole.Player))]
        [HttpGet]
        public Task<IActionResult> GetTeam()
        {
            return HandleUserIdAsync(async userId =>
            {
                var team=await _service.GetTeam(userId);
                return Ok(ApiResponseFactory.Success(team));
            });

        }

        [Authorize(Roles = nameof(UserRole.Player))]
        [HttpPost]
        [Route("swap")]
        public Task<IActionResult> SwapPlayers([FromBody] SwapPlayersDto dto)
        {
            return HandleUserIdAsync(async userId =>
            {
                await _service.Swap(userId,dto);
                return Ok(ApiResponseFactory.Success(true, "Players swapped successfully"));
            });
            
        }
    }
} 
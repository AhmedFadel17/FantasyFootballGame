using FantasyFootballGame.Application.DTOs.GameweekTeams;
using FantasyFootballGame.Application.Interfaces.GameweekTeams;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameweekTeamsController : ControllerBase
    {
        private readonly IGameweekTeamsService _service;
        public GameweekTeamsController(IGameweekTeamsService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromQuery] int fantasyTeamId)
        {
            var gameweekTeam = await _service.Create(fantasyTeamId);
            return Ok(gameweekTeam);
        }

        [HttpPost]
        [Route("swap")]
        public async Task<IActionResult> SwapPlayers([FromBody] SwapPlayersDto dto)
        {
            await _service.Swap(dto);
            return Ok("Players swapped successfully");
        }
    }
} 
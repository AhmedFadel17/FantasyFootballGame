using FantasyFootballGame.Application.DTOs.GameActions.Goals;
using FantasyFootballGame.Application.Interfaces.GameActions.Goals;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        private readonly IGoalsService _service;
        public GoalsController(IGoalsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var goal = await _service.GetById(id);
            return Ok(goal);
        }

        [HttpGet]
        [Route("by-fixture/{fixtureId:int}")]
        public async Task<IActionResult> GetByFixture(int fixtureId)
        {
            var goals = await _service.GetByFixture(fixtureId);
            return Ok(goals);
        }

        [HttpGet]
        [Route("by-gameweek/{gameweekId:int}")]
        public async Task<IActionResult> GetByGameweek(int gameweekId)
        {
            var goals = await _service.GetByGameweek(gameweekId);
            return Ok(goals);
        }

        [HttpGet]
        [Route("by-player/{playerId:int}")]
        public async Task<IActionResult> GetByPlayer(int playerId)
        {
            var goals = await _service.GetByPlayer(playerId);
            return Ok(goals);
        }

        [HttpGet]
        [Route("by-team/{teamId:int}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            var goals = await _service.GetByTeam(teamId);
            return Ok(goals);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGoalDto dto)
        {
            var goal = await _service.Update(id, dto);
            return Ok(goal);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGoalDto dto)
        {
            var goal = await _service.Create(dto);
            return Ok(goal);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Goal has been deleted");
        }
    }
} 
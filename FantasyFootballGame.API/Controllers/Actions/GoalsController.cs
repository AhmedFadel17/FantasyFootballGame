using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.DTOs.GameActions.Goals;
using FantasyFootballGame.Application.Interfaces.GameActions.Goals;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers.Actions
{
    [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
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
            return Ok(ApiResponseFactory.Success(goal));
        }

        [HttpGet]
        [Route("by-fixture/{fixtureId:int}")]
        public async Task<IActionResult> GetByFixture(int fixtureId)
        {
            var goals = await _service.GetByFixture(fixtureId);
            return Ok(ApiResponseFactory.Success(goals));
        }

        [HttpGet]
        [Route("by-gameweek/{gameweekId:int}")]
        public async Task<IActionResult> GetByGameweek(int gameweekId)
        {
            var goals = await _service.GetByGameweek(gameweekId);
            return Ok(ApiResponseFactory.Success(goals));
        }

        [HttpGet]
        [Route("by-player/{playerId:int}")]
        public async Task<IActionResult> GetByPlayer(int playerId)
        {
            var goals = await _service.GetByPlayer(playerId);
            return Ok(ApiResponseFactory.Success(goals));
        }

        [HttpGet]
        [Route("by-team/{teamId:int}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            var goals = await _service.GetByTeam(teamId);
            return Ok(ApiResponseFactory.Success(goals));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGoalDto dto)
        {
            var goal = await _service.Update(id, dto);
            return Ok(ApiResponseFactory.Success(goal, "goal updated successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGoalDto dto)
        {
            var goal = await _service.Create(dto);
            return Ok(ApiResponseFactory.Success(goal, "goal created successfully"));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok(ApiResponseFactory.Success(true, "goal has been deleted"));
        }
    }
}
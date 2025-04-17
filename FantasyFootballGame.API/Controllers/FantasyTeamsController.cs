using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.Interfaces.FantasyTeams;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FantasyTeamsController : BaseController
    {
        private readonly IFantasyTeamsService _service;
        public FantasyTeamsController(IFantasyTeamsService service)
        {
            _service = service;
        }

        [Authorize(Roles = nameof(UserRole.Moderator))]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var team = await _service.GetById(id);
            return Ok(ApiResponseFactory.Success(team));
        }

        [Authorize(Roles = nameof(UserRole.Moderator))]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok(ApiResponseFactory.Success(true, "Fantasy team has been deleted"));
        }

        [Authorize(Roles = nameof(UserRole.Player))]
        [HttpGet]
        [Route("my-team")]
        public Task<IActionResult> GetMyTeam()
        {
            return HandleUserIdAsync(async userId =>
            {
                var team = await _service.GetByUserId(userId);
                return Ok(ApiResponseFactory.Success(team));
            });
           
        }

        [Authorize(Roles = nameof(UserRole.Player))]
        [HttpPost]
        [Route("my-team")]
        public Task<IActionResult> CreateMyTeam([FromBody] CreateFantasyTeamDto dto)
        {
            return HandleUserIdAsync(async userId =>
            {
                var team = await _service.Create(userId, dto);
                return Ok(ApiResponseFactory.Success(team, "Fantasy team updated successfully"));
            });
        }

        [Authorize(Roles = nameof(UserRole.Player))]
        [HttpPut]
        [Route("my-team")]
        public Task<IActionResult> UpdateMyTeam([FromBody] UpdateFantasyTeamDto dto)
        {
            return HandleUserIdAsync(async userId =>
            {
                var team = await _service.Update(userId, dto);
                return Ok(ApiResponseFactory.Success(team, "Fantasy team updated successfully"));
            });   
        }

        [Authorize(Roles = nameof(UserRole.Player))]
        [HttpDelete]
        [Route("my-team")]
        public Task<IActionResult> DeleteMyTeam(int id)
        {
            return HandleUserIdAsync(async userId =>
            {
                await _service.DeleteByUserId(userId);
                return Ok(ApiResponseFactory.Success(true, "Fantasy team has been deleted"));
            });
        }
    }
} 
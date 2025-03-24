using FantasyFootballGame.Application.DTOs.Teams;
using FantasyFootballGame.Application.Interfaces.Teams;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamsService _service;
        public TeamsController(ITeamsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Roles = $"{nameof(UserRole.Player)}, {nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        public async Task<IActionResult> All()
        {
            var teams = await _service.All();
            return Ok(teams);
        }

        [HttpGet]
        [Authorize(Roles = $"{nameof(UserRole.Player)}, {nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var team = await _service.GetById(id);
            return Ok(team);
        }

        [HttpPut]
        [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateTeamDto dto)
        {
            var team = await _service.Update(id,dto);
            return Ok(team);
        }

        [HttpPost]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        public async Task<IActionResult> Create([FromBody] CreateTeamDto dto)
        {
            var team = await _service.Create(dto);
            return Ok(team);
        }

        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Team has been deleted");
        }
    }
}

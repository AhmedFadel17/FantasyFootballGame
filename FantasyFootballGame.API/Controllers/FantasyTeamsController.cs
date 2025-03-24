using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.Interfaces.FantasyTeams;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FantasyTeamsController : ControllerBase
    {
        private readonly IFantasyTeamsService _service;
        public FantasyTeamsController(IFantasyTeamsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var team = await _service.GetById(id);
            return Ok(team);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFantasyTeamDto dto)
        {
            var team = await _service.Update(id, dto);
            return Ok(team);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFantasyTeamDto dto)
        {
            var team = await _service.Create(dto);
            return Ok(team);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Fantasy team has been deleted");
        }
    }
} 
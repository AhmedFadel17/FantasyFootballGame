using FantasyFootballGame.Application.DTOs.Gameweeks;
using FantasyFootballGame.Application.Interfaces.Gameweeks;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class GameweeksController : ControllerBase
    {
        private readonly IGameweeksService _service;
        public GameweeksController(IGameweeksService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var gameweeks = await _service.All();
            return Ok(gameweeks);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var gameweek = await _service.GetById(id);
            return Ok(gameweek);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGameweekDto dto)
        {
            var gameweek = await _service.Update(id, dto);
            return Ok(gameweek);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameweekDto dto)
        {
            var gameweek = await _service.Create(dto);
            return Ok(gameweek);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Gameweek has been deleted");
        }
    }
} 
using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.DTOs.Gameweeks;
using FantasyFootballGame.Application.Interfaces.Gameweeks;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameweeksController : ControllerBase
    {
        private readonly IGameweeksService _service;
        public GameweeksController(IGameweeksService service)
        {
            _service = service;
        }

        [Authorize(Roles = $"{nameof(UserRole.Player)},{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var gameweeks = await _service.All();
            return Ok(ApiResponseFactory.Success(gameweeks));
        }

        [Authorize(Roles = $"{nameof(UserRole.Player)},{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var gameweek = await _service.GetById(id);
            return Ok(ApiResponseFactory.Success(gameweek));
        }

        [Authorize(Roles = $"{nameof(UserRole.Player)},{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpGet]
        [Route("current")]
        public async Task<IActionResult> GetCurrentGameweek()
        {
            var gameweek = await _service.GetCurrentGameweek();
            return Ok(ApiResponseFactory.Success(gameweek));
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateGameweekDto dto)
        {
            var gameweek = await _service.Update(id, dto);
            return Ok(ApiResponseFactory.Success(gameweek, "Gameweek updated successfully"));
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameweekDto dto)
        {
            var gameweek = await _service.Create(dto);
            return Ok(ApiResponseFactory.Success(gameweek, "Gameweek created successfully"));
        }

        [Authorize(Roles = nameof(UserRole.Moderator))]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok(ApiResponseFactory.Success(true, "Gameweek has been deleted"));
        }
    }
} 
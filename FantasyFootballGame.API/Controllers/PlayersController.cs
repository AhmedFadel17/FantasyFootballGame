using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.Interfaces.Players;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _service;
        public PlayersController(IPlayersService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var players = await _service.All();
            return Ok(players);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var player= await _service.GetById(id);
            return Ok(player);
        }

        [HttpGet]
        public async Task<IActionResult> GetByName([FromQuery] string name)
        {
            var player = await _service.GetByName(name);
            return Ok(player);
        }

        [HttpGet]
        public async Task<IActionResult> GetByPrice([FromQuery] double min,double max)
        {
            var player = await _service.GetByPrice(min,max);
            return Ok(player);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePlayerDto dto)
        {
            var player = await _service.Create(dto);
            return Ok(player);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdatePlayerDto dto)
        {
            var player = await _service.Update(id,dto);
            return Ok(player);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Player has been deleted");
        }
    }
}

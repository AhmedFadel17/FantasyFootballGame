using FantasyFootballGame.Application.DTOs.GameActions.Cards;
using FantasyFootballGame.Application.Interfaces.GameActions.Cards;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardsService _service;
        public CardsController(ICardsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var card = await _service.GetById(id);
            return Ok(card);
        }

        [HttpGet]
        [Route("fixture/{fixtureId:int}")]
        public async Task<IActionResult> GetByFixture(int fixtureId)
        {
            var cards = await _service.GetByFixture(fixtureId);
            return Ok(cards);
        }

        [HttpGet]
        [Route("gameweek/{gameweekId:int}")]
        public async Task<IActionResult> GetByGameweek(int gameweekId)
        {
            var cards = await _service.GetByGameweek(gameweekId);
            return Ok(cards);
        }

        [HttpGet]
        [Route("player/{playerId:int}")]
        public async Task<IActionResult> GetByPlayer(int playerId)
        {
            var cards = await _service.GetByPlayer(playerId);
            return Ok(cards);
        }

        [HttpGet]
        [Route("team/{teamId:int}")]
        public async Task<IActionResult> GetByTeam(int teamId)
        {
            var cards = await _service.GetByTeam(teamId);
            return Ok(cards);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCardDto dto)
        {
            var card = await _service.Update(id, dto);
            return Ok(card);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCardDto dto)
        {
            var card = await _service.Create(dto);
            return Ok(card);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Card has been deleted");
        }
    }
} 
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
using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.DTOs.GameActions.Cards;
using FantasyFootballGame.Application.Interfaces.GameActions.Cards;
using FantasyFootballGame.Domain.Enums.User;
using FantasyFootballGame.Domain.Models.Actions;
using FantasyFootballGame.Domain.Models.Actions.Goals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers.Actions
{
    [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
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
            return Ok(ApiResponseFactory.Success(card));
        }

        [HttpGet]
        public async Task<IActionResult> All(
            [FromQuery] int? teamId,
            int? gameweekId,
            int? playerId,
            int? fixtureId,
            int page = 1,
            int pageSize = 10)
        {
            var cards = await _service.GetAllWithPagination(page, pageSize, playerId, teamId, fixtureId, gameweekId);
            return Ok(ApiResponseFactory.Success(cards));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCardDto dto)
        {
            var card = await _service.Update(id, dto);
            return Ok(ApiResponseFactory.Success(card, "Card updated successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCardDto dto)
        {
            var card = await _service.Create(dto);
            return Ok(ApiResponseFactory.Success(card, "Card created successfully"));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok(ApiResponseFactory.Success(true, "Card has been deleted"));
        }
    }
}
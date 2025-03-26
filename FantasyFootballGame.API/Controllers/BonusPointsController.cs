using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints;
using FantasyFootballGame.Application.Interfaces.GameActions.BonusPoints;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BonusPointsController : ControllerBase
    {
        private readonly IBonusPointsService _service;
        public BonusPointsController(IBonusPointsService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var bonusPoints = await _service.GetById(id);
            return Ok(bonusPoints);
        }

        [HttpGet]
        [Route("fixture/{fixtureId:int}")]
        public async Task<IActionResult> GetByFixture(int fixtureId)
        {
            var bonusPoints = await _service.GetByFixture(fixtureId);
            return Ok(bonusPoints);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBonusPointsDto dto)
        {
            var bonusPoints = await _service.Update(id, dto);
            return Ok(bonusPoints);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBonusPointsDto dto)
        {
            var bonusPoints = await _service.Create(dto);
            return Ok(bonusPoints);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Bonus points have been deleted");
        }
    }
} 
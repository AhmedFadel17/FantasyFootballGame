using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints;
using FantasyFootballGame.Application.Interfaces.GameActions.BonusPoints;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers.Actions
{
    [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
    [Route("api/[controller]")]
    [ApiController]
    public class BonusPointsController : ControllerBase
    {
        private readonly IBonusPointsService _service;
        public BonusPointsController(IBonusPointsService service)
        {
            _service = service;
        }

        [Authorize(Roles = $"{nameof(UserRole.Player)},{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var bonusPoints = await _service.GetById(id);
            return Ok(ApiResponseFactory.Success(bonusPoints));
        }

        [Authorize(Roles = $"{nameof(UserRole.Player)},{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpGet]
        [Route("fixture/{fixtureId:int}")]
        public async Task<IActionResult> GetByFixture(int fixtureId)
        {
            var bonusPoints = await _service.GetByFixture(fixtureId);
            return Ok(ApiResponseFactory.Success(bonusPoints));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBonusPointsDto dto)
        {
            var bonusPoints = await _service.Update(id, dto);
            return Ok(ApiResponseFactory.Success(bonusPoints, "Bonus points updated successfully"));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBonusPointsDto dto)
        {
            var bonusPoints = await _service.Create(dto);
            return Ok(ApiResponseFactory.Success(bonusPoints, "Bonus points created successfully"));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok(ApiResponseFactory.Success(true, "Bonus points have been deleted"));
        }
    }
}
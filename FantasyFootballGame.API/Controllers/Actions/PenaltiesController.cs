using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties;
using FantasyFootballGame.Application.Interfaces.GameActions.Penalties;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers.Actions
{
    [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
    [Route("api/[controller]")]
    [ApiController]
    public class PenaltiesController : ControllerBase
    {
        private readonly IPenaltiesService _service;
        public PenaltiesController(IPenaltiesService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var penalty = await _service.GetById(id);
            return Ok(ApiResponseFactory.Success(penalty));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePenaltyDto dto)
        {
            var penalty = await _service.Create(dto);
            return Ok(ApiResponseFactory.Success(penalty,"Penalty created successfully"));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePenaltyDto dto)
        {
            var penalty = await _service.Update(id, dto);
            return Ok(ApiResponseFactory.Success(penalty, "Penalty updated successfully"));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok(ApiResponseFactory.Success(true, "Penalty has been deleted"));
        }
    }
}
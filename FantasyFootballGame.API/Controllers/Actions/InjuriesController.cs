using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.DTOs.GameActions.Injuries;
using FantasyFootballGame.Application.Interfaces.GameActions.Injuries;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers.Actions
{
    [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
    [Route("api/[controller]")]
    [ApiController]
    public class InjuriesController : ControllerBase
    {
        private readonly IInjuriesService _service;
        public InjuriesController(IInjuriesService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var injury = await _service.GetById(id);
            return Ok(ApiResponseFactory.Success(injury));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInjuryDto dto)
        {
            var injury = await _service.Create(dto);
            return Ok(ApiResponseFactory.Success(injury, "Injury created successfully"));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInjuryDto dto)
        {
            var injury = await _service.Update(id, dto);
            return Ok(ApiResponseFactory.Success(injury, "Injury updated successfully"));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok(ApiResponseFactory.Success(true, "Injury has been deleted"));
        }
    }
}
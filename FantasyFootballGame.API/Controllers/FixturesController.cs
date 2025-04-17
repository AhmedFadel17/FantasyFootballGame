using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.Application.Interfaces.Fixtures;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Authorize(Roles = nameof(UserRole.Admin))]
    [Route("api/[controller]")]
    [ApiController]
    public class FixturesController : ControllerBase
    {
        private readonly IFixturesService _service;
        public FixturesController(IFixturesService service)
        {
            _service = service;
        }

        [Authorize(Roles = $"{nameof(UserRole.Player)},{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var fixtures = await _service.All();
            return Ok(ApiResponseFactory.Success(fixtures));
        }

        [Authorize(Roles = $"{nameof(UserRole.Player)},{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var fixture = await _service.GetById(id);
            return Ok(ApiResponseFactory.Success(fixture));
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateFixtureDto dto)
        {
            var fixture = await _service.Update(id, dto);
            return Ok(ApiResponseFactory.Success(fixture, "Fixture updated successfully"));
        }

        [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFixtureDto dto)
        {
            var fixture = await _service.Create(dto);
            return Ok(ApiResponseFactory.Success(fixture, "Fixture created successfully"));
        }

        //[Authorize(Roles = $"{nameof(UserRole.Player)},{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        //[HttpPost]
        //[Route("{fixtureId:int}/teams/{teamId:int}/goals")]
        //public async Task<IActionResult> AddGoal(int fixtureId, int teamId)
        //{
        //    await _service.AddGoal(fixtureId, teamId);
        //    return Ok("Goal added successfully");
        //}

        //[HttpDelete]
        //[Route("{fixtureId:int}/teams/{teamId:int}/goals")]
        //public async Task<IActionResult> CancelGoal(int fixtureId, int teamId)
        //{
        //    await _service.CancelGoal(fixtureId, teamId);
        //    return Ok("Goal cancelled successfully");
        //}

        [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok(ApiResponseFactory.Success(true, "Fixture has been deleted"));
        }
    }
} 
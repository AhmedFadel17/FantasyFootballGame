using FantasyFootballGame.Application.DTOs.GameActions.Injuries;
using FantasyFootballGame.Application.Interfaces.GameActions.Injuries;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
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
            return Ok(injury);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInjuryDto dto)
        {
            var injury = await _service.Create(dto);
            return Ok(injury);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateInjuryDto dto)
        {
            var injury = await _service.Update(id, dto);
            return Ok(injury);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Injury has been deleted");
        }
    }
} 
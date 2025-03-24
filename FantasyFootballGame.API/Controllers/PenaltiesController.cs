using FantasyFootballGame.Application.DTOs.GameActions.Penalties;
using FantasyFootballGame.Application.Interfaces.GameActions.Penalties;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
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
            return Ok(penalty);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePenaltyDto dto)
        {
            var penalty = await _service.Create(dto);
            return Ok(penalty);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdatePenaltyDto dto)
        {
            var penalty = await _service.Update(id, dto);
            return Ok(penalty);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Penalty has been deleted");
        }
    }
} 
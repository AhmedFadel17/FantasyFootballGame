using FantasyFootballGame.Application.DTOs.GameActions.Saves;
using FantasyFootballGame.Application.Interfaces.GameActions.Saves;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavesController : ControllerBase
    {
        private readonly ISavesService _service;
        public SavesController(ISavesService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var save = await _service.GetById(id);
            return Ok(save);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaveDto dto)
        {
            var save = await _service.Create(dto);
            return Ok(save);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateSaveDto dto)
        {
            var save = await _service.Update(id, dto);
            return Ok(save);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Save has been deleted");
        }
    }
} 
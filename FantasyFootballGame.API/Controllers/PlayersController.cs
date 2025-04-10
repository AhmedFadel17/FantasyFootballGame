using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.Interfaces.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FantasyFootballGame.Domain.Enums.User;
using FantasyFootballGame.Domain.Enums;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersService _service;
        public PlayersController(IPlayersService service)
        {
            _service = service;
        }



        [HttpGet]
        [Authorize(Roles = $"{nameof(UserRole.Player)}, {nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        public async Task<IActionResult> GetAll([FromQuery] int page,int pageSize,int? teamId,int? shirtNumber, string? name,PlayerStatus? status, PlayerPosition? position, double? minPrice, double? maxPrice)
        {
            var players = await _service.AllWithPaginationAndFilters(page,pageSize,teamId,shirtNumber,name,status,position,minPrice,maxPrice);
            return Ok(players);
        }

        [HttpGet]
        [Authorize(Roles = $"{nameof(UserRole.Player)}, {nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var player= await _service.GetById(id);
            return Ok(player);
        }


        [HttpPost]
        [Authorize(Roles = $"{nameof(UserRole.Player)}, {nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        public async Task<IActionResult> Create([FromBody] CreatePlayerDto dto)
        {
            var player = await _service.Create(dto);
            return Ok(player);
        }

        [HttpPut]
        [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdatePlayerDto dto)
        {
            var player = await _service.Update(id,dto);
            return Ok(player);
        }

        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok("Player has been deleted");
        }
    }
}

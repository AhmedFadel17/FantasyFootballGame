﻿using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Application.Interfaces.Players;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using FantasyFootballGame.Domain.Enums.User;
using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.API.Factories;

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
        public async Task<IActionResult> GetAll(
            [FromQuery] int? teamId,
            int? shirtNumber, 
            string? name,
            PlayerStatus? status, 
            PlayerPosition? position, 
            double? minPrice, 
            double? maxPrice, 
            int page = 1, 
            int pageSize = 10
            )
        {
            var players = await _service.AllWithPaginationAndFilters(page,pageSize,teamId,shirtNumber,name,status,position,minPrice,maxPrice);
            return Ok(ApiResponseFactory.Success(players));
        }

        [HttpGet]
        [Authorize(Roles = $"{nameof(UserRole.Player)}, {nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var player= await _service.GetById(id);
            return Ok(ApiResponseFactory.Success(player));
        }


        [HttpPost]
        [Authorize(Roles = $" {nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        public async Task<IActionResult> Create([FromBody] CreatePlayerDto dto)
        {
            var player = await _service.Create(dto);
            return Ok(ApiResponseFactory.Success(player, "Player created successfully"));
        }

        [HttpPut]
        [Authorize(Roles = $"{nameof(UserRole.Admin)} , {nameof(UserRole.Moderator)}")]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id,[FromBody] UpdatePlayerDto dto)
        {
            var player = await _service.Update(id,dto);
            return Ok(ApiResponseFactory.Success(player, "Player updated successfully"));
        }

        [HttpDelete]
        [Authorize(Roles = nameof(UserRole.Moderator))]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return Ok(ApiResponseFactory.Success(true, "Player has been deleted"));
        }
    }
}

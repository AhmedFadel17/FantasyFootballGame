using FantasyFootballGame.API.Factories;
using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.Interfaces.Transfers;
using FantasyFootballGame.Domain.Enums.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FantasyFootballGame.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : BaseController
    {
        private readonly ITransfersService _service;
        public TransfersController(ITransfersService transfersService)
        {
            _service = transfersService;   
        }

        [Authorize(Roles = nameof(UserRole.Player))]
        [HttpPost]
        public Task<IActionResult> makeTransfers(MakeTransfersDto dto)
        {
            return HandleUserIdAsync(async userId =>
            {
                await _service.Create(userId,dto);
                return Ok(ApiResponseFactory.Success("Transfers has been made successfully"));
            });
        }
    }
}

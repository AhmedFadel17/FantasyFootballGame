using FantasyFootballGame.Application.DTOs.GameweekTeams;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.Interfaces.GameweekTeams
{
    public interface IGameweekTeamsService
    {
        Task<GameweekTeam> Create(int fantasyTeamId);
        Task Swap(Guid userId,SwapPlayersDto dto);
        Task<GameweekTeamResponseDto> GetTeam(Guid userId);


    }
}

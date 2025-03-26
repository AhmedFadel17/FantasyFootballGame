using FantasyFootballGame.Application.DTOs.PlayersStats;

namespace FantasyFootballGame.Application.Interfaces.PlayersStats
{
    public interface IPlayersStatsService
    {
        Task<List<PlayerStatsResponseDto>> All();
        Task<PlayerStatsResponseDto> GetById(int id);
        Task<PlayerStatsResponseDto> Update(int id, UpdatePlayerStatsDto dto);
        Task<PlayerStatsResponseDto> Create(CreatePlayerStatsDto dto);
        Task Delete(int id);
    }
} 
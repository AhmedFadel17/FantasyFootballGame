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
        Task<List<TopStatResponseDto>> GetTopGoalScorers(int limit);
        Task<List<TopStatResponseDto>> GetTopAssists(int limit);
        Task<List<TopStatResponseDto>> GetTopCleanSheets(int limit);
        Task<List<TopStatResponseDto>> GetTopMinutesPlayed(int limit);
        Task<List<TopStatResponseDto>> GetTopSaves(int limit);
        Task<List<TopStatResponseDto>> GetTopTotalPoints(int limit);
    }
} 
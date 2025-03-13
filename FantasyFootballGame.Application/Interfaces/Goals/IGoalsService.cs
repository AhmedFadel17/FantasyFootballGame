using FantasyFootballGame.Application.DTOs.Goals;

namespace FantasyFootballGame.Application.Interfaces.Goals
{
    public interface IGoalsService
    {
        Task<GoalResponseDto> GetById(int id);
        Task<List<GoalResponseDto>> GetByFixture(int fixtureId);
        Task<List<GoalResponseDto>> GetByGameweek(int gameweekId);
        Task<List<GoalResponseDto>> GetByPlayer(int playerId);
        Task<List<GoalResponseDto>> GetByTeam(int teamId);
        Task<GoalResponseDto> Update(int id, UpdateGoalDto dto);
        Task<GoalResponseDto> Create(CreateGoalDto dto);
        Task Delete(int id);
    }
}

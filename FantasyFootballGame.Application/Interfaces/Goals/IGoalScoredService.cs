using FantasyFootballGame.Application.DTOs.Goals.GoalsScored;

namespace FantasyFootballGame.Application.Interfaces.Goals
{
    public interface IGoalScoredService
    {
        Task<GoalScoredResponseDto> GetById(int id);
        Task<GoalScoredResponseDto> Update(int id, UpdateGoalScoredDto dto);
        Task<GoalScoredResponseDto> Create(CreateGoalScoredDto dto);
    }
}

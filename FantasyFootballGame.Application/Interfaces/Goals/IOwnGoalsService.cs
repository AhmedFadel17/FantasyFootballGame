using FantasyFootballGame.Application.DTOs.Goals.OwnGoals;

namespace FantasyFootballGame.Application.Interfaces.Goals
{
    public interface IOwnGoalsService
    {
        Task<OwnGoalResponseDto> GetById(int id);
        Task<OwnGoalResponseDto> Update(int id, UpdateOwnGoalDto dto);
        Task<OwnGoalResponseDto> Create(CreateOwnGoalDto dto);
    }
}

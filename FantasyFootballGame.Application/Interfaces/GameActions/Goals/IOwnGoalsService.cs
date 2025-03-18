using FantasyFootballGame.Application.DTOs.GameActions.Goals.OwnGoals;

namespace FantasyFootballGame.Application.Interfaces.GameActions.Goals
{
    public interface IOwnGoalsService
    {
        Task<OwnGoalResponseDto> GetById(int id);
        Task<OwnGoalResponseDto> Update(int id, UpdateOwnGoalDto dto);
        Task<OwnGoalResponseDto> Create(CreateOwnGoalDto dto);
    }
}

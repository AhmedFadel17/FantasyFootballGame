using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed;

namespace FantasyFootballGame.Application.Interfaces.GameActions.Penalties
{
    public interface IPenaltiesMissedService
    {
        Task<PenaltyMissedResponseDto> GetById(int id);
        Task<PenaltyMissedResponseDto> Update(int id, UpdatePenaltyMissedDto dto);
        Task<PenaltyMissedResponseDto> Create(CreatePenaltyMissedDto dto);
        Task Delete(int id);
    }
}

using FantasyFootballGame.Application.DTOs.GameActions.Penalties;

namespace FantasyFootballGame.Application.Interfaces.GameActions.Penalties
{
    public interface IPenaltiesService
    {
        Task<PenaltyResponseDto> GetById(int id);
        Task<PenaltyResponseDto> Update(int id, UpdatePenaltyDto dto);
        Task<PenaltyResponseDto> Create(CreatePenaltyDto dto);
        Task Delete(int id);
    }
}

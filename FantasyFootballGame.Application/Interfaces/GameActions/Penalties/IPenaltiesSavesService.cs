using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves;

namespace FantasyFootballGame.Application.Interfaces.GameActions.Penalties
{
    public interface IPenaltiesSavesService
    {
        Task<PenaltySaveResponseDto> GetById(int id);
        Task<PenaltySaveResponseDto> Update(int id, UpdatePenaltySaveDto dto);
        Task<PenaltySaveResponseDto> Create(CreatePenaltySaveDto dto);
        Task Delete(int id);
    }
}

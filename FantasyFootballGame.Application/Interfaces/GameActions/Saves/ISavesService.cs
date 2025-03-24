using FantasyFootballGame.Application.DTOs.GameActions.Saves;

namespace FantasyFootballGame.Application.Interfaces.GameActions.Saves
{
    public interface ISavesService
    {
        Task<SaveResponseDto> GetById(int id);
        Task<SaveResponseDto> Update(int id, UpdateSaveDto dto);
        Task<SaveResponseDto> Create(CreateSaveDto dto);
        Task Delete(int id);
    }
}

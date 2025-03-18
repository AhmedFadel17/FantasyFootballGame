using FantasyFootballGame.Application.DTOs.GameActions.Goals.Assists;

namespace FantasyFootballGame.Application.Interfaces.GameActions.Goals
{
    public interface IAssistsService
    {
        Task<AssistResponseDto> GetById(int id);
        Task<AssistResponseDto> Update(int id, UpdateAssistDto dto);
        Task<AssistResponseDto> Create(CreateAssistDto dto);
        Task Delete(int id);
    }
}

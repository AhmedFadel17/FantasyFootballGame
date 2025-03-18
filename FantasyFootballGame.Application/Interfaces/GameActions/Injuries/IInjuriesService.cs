using FantasyFootballGame.Application.DTOs.GameActions.Injuries;

namespace FantasyFootballGame.Application.Interfaces.GameActions.Injuries
{
    public interface IInjuriesService
    {
        Task<InjuryResponseDto> GetById(int id);
        Task<InjuryResponseDto> Update(int id, UpdateInjuryDto dto);
        Task<InjuryResponseDto> Create(CreateInjuryDto dto);
        Task Delete(int id);
    }
}

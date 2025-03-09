using FantasyFootballGame.Application.DTOs.Gameweeks;

namespace FantasyFootballGame.Application.Interfaces.Gameweeks
{
    public interface IGameweeksService
    {
        Task<List<GameweekResponseDto>> All();
        Task<GameweekResponseDto> GetById(int id);
        Task<GameweekResponseDto> Update(int id, UpdateGameweekDto dto);
        Task<GameweekResponseDto> Create(CreateGameweekDto dto);
        Task Delete(int id);
    }
}

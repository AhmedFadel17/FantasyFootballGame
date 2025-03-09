using FantasyFootballGame.Application.DTOs.Players;

namespace FantasyFootballGame.Application.Interfaces.Players
{
    public interface IPlayersService
    {
        Task<List<PlayerResponseDto>> All();
        Task<PlayerResponseDto> GetById(int id);
        Task<List<PlayerResponseDto>> GetByName(string name);
        Task<List<PlayerResponseDto>> GetByPrice(double min,double max);
        Task<PlayerResponseDto> Update(int id, UpdatePlayerDto dto);
        Task<PlayerResponseDto> Create(CreatePlayerDto dto);
        Task Delete(int id);
    }
}

using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Domain.Enums;

namespace FantasyFootballGame.Application.Interfaces.Players
{
    public interface IPlayersService
    {
        Task<List<PlayerResponseDto>> All();
        Task<PaginationDto<PlayerResponseDto>> AllWithPaginationAndFilters(int page, int pageSize,int? teamId,int? shirtNumber, string? name,PlayerStatus? status, PlayerPosition? position, double? minPrice, double? maxPrice);
        Task<PlayerResponseDto> GetById(int id);
        Task<List<PlayerResponseDto>> GetByName(string name);
        Task<List<PlayerResponseDto>> GetByPrice(double min,double max);
        Task<PlayerResponseDto> Update(int id, UpdatePlayerDto dto);
        Task<PlayerResponseDto> Create(CreatePlayerDto dto);
        Task Delete(int id);
    }
}

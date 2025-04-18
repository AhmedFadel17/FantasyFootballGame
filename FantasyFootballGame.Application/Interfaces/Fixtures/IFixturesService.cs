using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Domain.Enums;

namespace FantasyFootballGame.Application.Interfaces.Fixtures
{
    public interface IFixturesService
    {
        Task<List<FixtureResponseDto>> All();
        Task<FixtureResponseDto> GetById(int id);
        Task<FixtureResponseDto> Update(int id, UpdateFixtureDto dto);
        Task<FixtureResponseDto> Create(CreateFixtureDto dto);
        Task AddGoal(int fixtureId, int teamId);
        Task CancelGoal(int fixtureId, int teamId);
        Task Delete(int id);

        Task<PaginationDto<FixtureResponseDto>> AllWithPagination(
            int page,
            int pageSize,
            int? teamId,
            int? gameweekId, 
            int? playerId,
            DateOnly? date
            );

    }
}

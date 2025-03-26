using FantasyFootballGame.Application.DTOs.Fixtures;

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
        Task<List<FixtureResponseDto>> GetByGameweek(int gameweekId);
        Task<List<FixtureResponseDto>> GetByTeam(int teamId);
    }
}

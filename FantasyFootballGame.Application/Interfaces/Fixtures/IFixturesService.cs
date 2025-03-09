using FantasyFootballGame.Application.DTOs.Fixtures;

namespace FantasyFootballGame.Application.Interfaces.Fixtures
{
    public interface IFixturesService
    {
        Task<List<FixtureResponseDto>> All();
        Task<FixtureResponseDto> GetById(int id);
        Task<FixtureResponseDto> Update(int id, UpdateFixtureDto dto);
        Task<FixtureResponseDto> Create(CreateFixtureDto dto);
        Task Delete(int id);
    }
}

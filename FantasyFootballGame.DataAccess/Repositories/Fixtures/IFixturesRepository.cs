using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.Fixtures
{
    public interface IFixturesRepository : IBaseRepository<Fixture>
    {
        Task<IEnumerable<Fixture>> GetByGameweek(int gameweekId);
        Task<IEnumerable<Fixture>> GetByTeam(int teamId);
    }
}

using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.Fixtures
{
    public interface IFixturesRepository : IBaseRepository<Fixture>
    {
        Task<IEnumerable<Fixture>> GetByGameweek(int gameweekId);
        Task<IEnumerable<Fixture>> GetByTeam(int teamId);
        public Task<(IEnumerable<Fixture>, int)> GetAllWithPagination(int page,
            int pageSize,
            int? teamId,
            int? gameweekId,
            int? playerId,
            DateOnly? date
            );

    }
}

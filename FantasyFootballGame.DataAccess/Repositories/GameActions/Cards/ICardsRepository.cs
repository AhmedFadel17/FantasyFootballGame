using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.RedCards
{
    public interface ICardsRepository : IBaseRepository<Card>
    {
        Task<IEnumerable<Card>> GetByFixture(int fixtureId);
        Task<IEnumerable<Card>> GetByGameweek(int gameweekId);
        Task<IEnumerable<Card>> GetByPlayer(int playerId);
        Task<IEnumerable<Card>> GetByTeam(int teamId);
        public Task<(IEnumerable<Card>, int)> GetAllWithPagination(
            int page,
            int pageSize,
            int? teamId,
            int? fixtureId,
            int? gameweekId,
            int? playerId
            );
    }
}

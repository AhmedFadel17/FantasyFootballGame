using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.RedCards
{
    public interface ICardsRepository : IBaseRepository<Card>
    {
        Task<IEnumerable<Card>> GetByFixture(int fixtureId);
        Task<IEnumerable<Card>> GetByGameweek(int gameweekId);
        Task<IEnumerable<Card>> GetByPlayer(int playerId);
        Task<IEnumerable<Card>> GetByTeam(int teamId);
    }
}

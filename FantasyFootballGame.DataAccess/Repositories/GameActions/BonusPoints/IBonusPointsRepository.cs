using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.BonusPoints
{
    public interface IBonusPointsRepository : IBaseRepository<Bonus>
    {
        Task<IEnumerable<Bonus>> GetByFixture(int  fixtureId);
    }
}

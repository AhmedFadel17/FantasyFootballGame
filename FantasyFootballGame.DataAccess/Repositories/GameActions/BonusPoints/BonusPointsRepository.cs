using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.BonusPoints
{
    public class BonusPointsRepository : BaseRepository<Bonus>, IBonusPointsRepository
    {
        public BonusPointsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Bonus>> GetByFixture(int fixtureId)
        {
            return await _dbSet.Where(b => b.FixtureId==fixtureId).ToListAsync();
        }
    }
}

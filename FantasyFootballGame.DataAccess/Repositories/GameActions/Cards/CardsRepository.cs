using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.RedCards
{
    public class CardsRepository : BaseRepository<Card>, ICardsRepository
    {
        public CardsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Card>> GetByFixture(int fixtureId)
        {
            return await _dbSet.Where(c => c.FixtureId == fixtureId).ToListAsync();
        }

        public async Task<IEnumerable<Card>> GetByGameweek(int gameweekId)
        {
            return await _dbSet.Where(c => c.Fixture.GameweekId == gameweekId).ToListAsync();
        }

        public async Task<IEnumerable<Card>> GetByPlayer(int playerId)
        {
            return await _dbSet.Where(c => c.PlayerId == playerId).ToListAsync();
        }

        public async Task<IEnumerable<Card>> GetByTeam(int teamId)
        {
            return await _dbSet.Where(c => c.TeamId == teamId).ToListAsync();
        }
    }
}

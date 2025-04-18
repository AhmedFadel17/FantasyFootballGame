using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.DataAccess.Extentions.Actions.Cards;
using FantasyFootballGame.DataAccess.Extentions.Common;
using FantasyFootballGame.Domain.Models.Actions;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.RedCards
{
    public class CardsRepository : BaseRepository<Card>, ICardsRepository
    {
        public CardsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Card>, int)> GetAllWithPagination(int page, int pageSize, int? teamId,int? fixtureId, int? gameweekId, int? playerId)
        {
            var query = _dbSet.AsQueryable()
            .Include(c => c.Player)
            .FilterByTeam(teamId)
            .FilterByGameweek(gameweekId)
            .FilterByFixture(fixtureId)
            .FilterByPlayer(playerId);
            var totalCount = await query.CountAsync();
            var fixtures = await query.Paginate(page, pageSize).ToListAsync();

            return (fixtures, totalCount);
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

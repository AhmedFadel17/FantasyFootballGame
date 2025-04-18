using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.DataAccess.Extentions.Common;
using FantasyFootballGame.DataAccess.Extentions.Fixtures;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Fixtures
{
    public class FixturesRepository : BaseRepository<Fixture>, IFixturesRepository
    {
        public FixturesRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<(IEnumerable<Fixture>, int)> GetAllWithPagination(int page, int pageSize, int? teamId, int? gameweekId, int? playerId,DateOnly? date)
        {
            var query = _dbSet.AsQueryable()
            .FilterByTeam(teamId)
            .FilterByGameweek(gameweekId)
            .FilterByDay(date)
            .FilterByPlayer(playerId);
            var totalCount = await query.CountAsync();
            var fixtures = await query.Paginate(page, pageSize).ToListAsync();

            return (fixtures, totalCount);
        }

        public async Task<IEnumerable<Fixture>> GetByGameweek(int gameweekId)
        {
            return await _dbSet.Where(f => f.GameweekId == gameweekId).ToListAsync();
        }

        public async Task<IEnumerable<Fixture>> GetByTeam(int teamId)
        {
            return await _dbSet.Where(f => f.HomeTeamId == teamId || f.AwayTeamId == teamId).ToListAsync();
        }
    }
}

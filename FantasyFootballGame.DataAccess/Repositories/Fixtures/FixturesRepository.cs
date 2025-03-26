using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Fixtures
{
    public class FixturesRepository : BaseRepository<Fixture>, IFixturesRepository
    {
        public FixturesRepository(AppDbContext context) : base(context)
        {
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

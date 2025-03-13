using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using FantasyFootballGame.Domain.Models.Actions;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.Goals
{
    public class GoalsRepository : BaseRepository<Goal>, IGoalsRepository
    {
        public GoalsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Goal>> GetByFixture(int fixtureId)
        {
            return await _dbSet.Where(g=> g.FixtureId == fixtureId).ToListAsync();
        }

        public async Task<IEnumerable<Goal>> GetByGameweek(int gameweekId)
        {
            return await _dbSet.Where(g => g.Fixture.GameweekId == gameweekId).ToListAsync();
        }

        public async Task<IEnumerable<Goal>> GetByPlayer(int playerId)
        {
            return await _dbSet.Where(g => g.GoalScored.PlayerId == playerId).ToListAsync();
        }

        public async Task<IEnumerable<Goal>> GetByTeam(int teamId)
        {
            return await _dbSet.Where(g => g.TeamId == teamId).ToListAsync();

        }
    }
}

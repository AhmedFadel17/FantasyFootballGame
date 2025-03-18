using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions.Goals;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.GoalsScored
{
    public class GoalsScoredRepository : BaseRepository<GoalScored>, IGoalsScoredRepository
    {
        public GoalsScoredRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckGoalHasScored(int goalId)
        {
            return await _dbSet.AnyAsync(g =>  g.GoalId == goalId);
        }
    }
}

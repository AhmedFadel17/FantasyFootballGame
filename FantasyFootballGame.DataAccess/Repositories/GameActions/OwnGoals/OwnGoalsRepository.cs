using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.OwnGoals
{
    public class OwnGoalsRepository : BaseRepository<OwnGoal>, IOwnGoalsRepository
    {
        public OwnGoalsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckGoalHasOwnGoal(int goalId)
        {
            return await _dbSet.AnyAsync(g => g.GoalId == goalId);
        }
    }
}

using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.Assists
{
    public class AssistsRepository : BaseRepository<Assist>, IAssistsRepository
    {
        public AssistsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<bool> CheckGoalHasAssist(int goalId)
        {
            return await _dbSet.AnyAsync(a => a.GoalId == goalId);
        }

    }
}

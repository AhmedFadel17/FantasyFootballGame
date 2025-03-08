using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.GoalsScored
{
    public class GoalsScoredRepository : BaseRepository<GoalScored>, IGoalsScoredRepository
    {
        public GoalsScoredRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using FantasyFootballGame.Domain.Models.Actions.Goals;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.OwnGoals
{
    public interface IOwnGoalsRepository : IBaseRepository<OwnGoal>
    {
        Task<bool> CheckGoalHasOwnGoal(int goalId);
    }
}

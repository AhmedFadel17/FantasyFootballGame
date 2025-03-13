using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.GoalsScored
{
    public interface IGoalsScoredRepository : IBaseRepository<GoalScored>
    {
        Task<bool> CheckGoalHasScored(int goalId);
    }
}

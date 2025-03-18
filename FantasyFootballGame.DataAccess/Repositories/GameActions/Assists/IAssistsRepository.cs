using FantasyFootballGame.Domain.Models.Actions.Goals;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.Assists
{
    public interface IAssistsRepository : IBaseRepository<Assist>
    {
        Task<bool> CheckGoalHasAssist(int goalId);
    }
}

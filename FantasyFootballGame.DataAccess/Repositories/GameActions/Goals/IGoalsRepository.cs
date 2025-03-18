using FantasyFootballGame.Domain.Models.Actions.Goals;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.Goals
{
    public interface IGoalsRepository : IBaseRepository<Goal>
    {
        Task<IEnumerable<Goal>> GetByFixture(int fixtureId);
        Task<IEnumerable<Goal>> GetByGameweek(int gameweekId);
        Task<IEnumerable<Goal>> GetByPlayer(int playerId);
        Task<IEnumerable<Goal>> GetByTeam(int teamId);
    }
}

using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.FantasyTeams
{
    public interface IFantasyTeamsRepository : IBaseRepository<FantasyTeam>
    {
        Task<FantasyTeam> GetByUserId(int userId);
    }
}

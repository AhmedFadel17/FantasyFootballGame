using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.GameweekTeams
{
    public interface IGameweekTeamsRepository : IBaseRepository<GameweekTeam>
    {
        Task<GameweekTeam> GetCurrentGameweekTeam(int fantasyTeamId, int gameweekId);
    }
}

using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.FantasyTeamPlayers
{
    public interface IFanatsyTeamPlayersRepository : IBaseRepository<FantasyTeamPlayer>
    {
        Task<FantasyTeamPlayer> GetPlayerFromTeam(int teamId,int playerId);
        Task<IEnumerable<FantasyTeamPlayer>> GetByTeam(int teamId);

    }
}

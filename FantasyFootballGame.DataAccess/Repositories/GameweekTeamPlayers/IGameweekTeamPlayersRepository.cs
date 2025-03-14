using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.GameweekTeamPlayers
{
    public interface IGameweekTeamPlayersRepository : IBaseRepository<GameweekTeamPlayer>
    {
        Task<GameweekTeamPlayer> GetPlayerFromTeam(int gameweekTeamId, int playerId);

    }
}

using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.PlayersStats
{
    public interface IPlayersStatsRepository : IBaseRepository<PlayerStat>
    {
        Task<IEnumerable<TopStat>> GetTopGoalScorers(int limit);

    }
}

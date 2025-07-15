using FantasyFootballGame.Domain.Models;
using System.Linq.Expressions;

namespace FantasyFootballGame.DataAccess.Repositories.PlayersStats
{
    public interface IPlayersStatsRepository : IBaseRepository<PlayerStat>
    {
        Task<IEnumerable<TopStat>> GetTopPlayersByStat(Expression<Func<PlayerStat, int>> statSelector, int limit);

    }
}

using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FantasyFootballGame.DataAccess.Repositories.PlayersStats
{
    public class PlayersStatsRepository : BaseRepository<PlayerStat>, IPlayersStatsRepository
    {
        public PlayersStatsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TopStat>> GetTopPlayersByStat(
    Expression<Func<PlayerStat, int>> statSelector,
    int limit)
        {
            var list = await _dbSet
                .Include(s => s.Player)
                .ThenInclude(p => p.Team)
                .OrderByDescending(statSelector)
                .Take(limit)
                .Select(s => new TopStat
                {
                    Stat = EF.Property<int>(s, ((MemberExpression)statSelector.Body).Member.Name),
                    Player = s.Player
                })
                .ToListAsync();

            return list;
        }


    }
}

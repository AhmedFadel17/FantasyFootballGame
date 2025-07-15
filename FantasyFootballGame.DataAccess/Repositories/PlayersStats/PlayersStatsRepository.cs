using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.PlayersStats
{
    public class PlayersStatsRepository : BaseRepository<PlayerStat>, IPlayersStatsRepository
    {
        public PlayersStatsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TopStat>> GetTopGoalScorers(int limit)
        {
            var list = await _dbSet
                .Include(s => s.Player)
                .ThenInclude(p => p.Team)
                .OrderByDescending(s => s.GoalsScored)
                .Take(limit)
                .Select(s => new TopStat
                {
                    Stat = s.GoalsScored,
                    Player = s.Player
                })
                .ToListAsync();

            return list;
        }

    }
}

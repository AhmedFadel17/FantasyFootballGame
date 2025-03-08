using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.PlayersStats
{
    public class PlayersStatsRepository : BaseRepository<PlayerGameweekForm>, IPlayersStatsRepository
    {
        public PlayersStatsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

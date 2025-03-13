using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.FantasyTeamPlayers
{
    public class FantasyTeamPlayerRepository : BaseRepository<FantasyTeamPlayer>, IFanatsyTeamPlayersRepository
    {
        public FantasyTeamPlayerRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.Players
{
    public class PlayersRepository : BaseRepository<Player>, IPlayersRepository
    {
        public PlayersRepository(AppDbContext context) : base(context)
        {
        }
    }
}

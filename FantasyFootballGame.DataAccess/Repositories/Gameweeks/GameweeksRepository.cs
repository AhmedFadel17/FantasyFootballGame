using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.Gameweeks
{
    public class GameweeksRepository : BaseRepository<Gameweek>, IGameweeksRepository
    {
        public GameweeksRepository(AppDbContext context) : base(context)
        {
        }
    }
}

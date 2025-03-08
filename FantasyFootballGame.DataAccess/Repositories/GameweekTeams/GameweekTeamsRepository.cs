using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.GameweekTeams
{
    public class GameweekTeamsRepository : BaseRepository<GameweekTeam>, IGameweekTeamsRepository
    {
        public GameweekTeamsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.FantasyTeams
{
    public class FantasyTeamsRepository : BaseRepository<FantasyTeam>, IFantasyTeamsRepository
    {
        public FantasyTeamsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

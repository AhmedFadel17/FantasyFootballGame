using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.Teams
{
    public class TeamsRepository : BaseRepository<Team>, ITeamsRepository
    {
        public TeamsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

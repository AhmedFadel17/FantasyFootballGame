using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.OwnGoals
{
    public class OwnGoalsRepository : BaseRepository<OwnGoal>, IOwnGoalsRepository
    {
        public OwnGoalsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

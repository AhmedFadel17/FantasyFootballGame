using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.Goals
{
    public class GoalsRepository : BaseRepository<Goal>, IGoalsRepository
    {
        public GoalsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

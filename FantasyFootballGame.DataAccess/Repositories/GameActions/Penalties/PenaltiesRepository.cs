using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions.Penalties;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.Penalties
{
    public class PenaltiesRepository : BaseRepository<Penalty>, IPenaltiesRepository
    {
        public PenaltiesRepository(AppDbContext context) : base(context)
        {
        }
    }
}

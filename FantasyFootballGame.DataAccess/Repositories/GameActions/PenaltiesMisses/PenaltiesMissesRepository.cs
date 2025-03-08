using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesMisses
{
    public class PenaltiesMissesRepository : BaseRepository<PenaltyMiss>, IPenaltiesMissesRepository
    {
        public PenaltiesMissesRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions.Penalties;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesMissed
{
    public class PenaltiesMissedRepository : BaseRepository<PenaltyMiss>, IPenaltiesMissedRepository
    {
        public PenaltiesMissedRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PenaltyMiss> GetMissedPenaltyByPenaltyId(int penaltyId)
        {
            return await _dbSet.Where(p => p.PenaltyId == penaltyId).FirstAsync();

        }
    }
}

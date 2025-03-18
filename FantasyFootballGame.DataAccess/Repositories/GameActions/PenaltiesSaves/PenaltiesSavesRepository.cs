using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions.Penalties;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesSaves
{
    public class PenaltiesSavesRepository : BaseRepository<PenaltySave>, IPenaltiesSavesRepository
    {
        public PenaltiesSavesRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<PenaltySave> GetPenaltySaveByPenaltyId(int penaltyId)
        {
            return await _dbSet.Where(p => p.PenaltyId == penaltyId).FirstAsync();

        }
    }
}

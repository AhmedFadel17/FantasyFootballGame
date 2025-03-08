using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesSaves
{
    public class PenaltiesSavesRepository : BaseRepository<PenaltySave>, IPenaltiesSavesRepository
    {
        public PenaltiesSavesRepository(AppDbContext context) : base(context)
        {
        }
    }
}

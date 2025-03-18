using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.GameActions.CleanSheets
{
    public class CleanSheetsRepository : BaseRepository<CleanSheet>, ICleanSheetsRepository
    {
        public CleanSheetsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

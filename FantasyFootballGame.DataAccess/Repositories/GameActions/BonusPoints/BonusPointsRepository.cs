using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.BonusPoints
{
    public class BonusPointsRepository : BaseRepository<Bonus>, IBonusPointsRepository
    {
        public BonusPointsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

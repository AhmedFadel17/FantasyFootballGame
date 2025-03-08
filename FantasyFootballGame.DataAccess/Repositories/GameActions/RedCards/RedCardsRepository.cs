using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.RedCards
{
    public class RedCardsRepository : BaseRepository<RedCard>, IRedCardsRepository
    {
        public RedCardsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

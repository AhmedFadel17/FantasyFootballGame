using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.YellowCards
{
    public class YellowCardsRepository : BaseRepository<YellowCard>, IYellowCardsRepository
    {
        public YellowCardsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

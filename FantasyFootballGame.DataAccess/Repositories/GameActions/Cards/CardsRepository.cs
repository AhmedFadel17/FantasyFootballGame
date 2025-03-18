using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.RedCards
{
    public class CardsRepository : BaseRepository<Card>, ICardsRepository
    {
        public CardsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.Injuries
{
    public class InjuriesRepository : BaseRepository<Injury>, IInjuriesRepository
    {
        public InjuriesRepository(AppDbContext context) : base(context)
        {
        }
    }
}

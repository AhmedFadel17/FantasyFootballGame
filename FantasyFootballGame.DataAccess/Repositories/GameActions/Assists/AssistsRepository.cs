using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Repositories.Actions.Assists
{
    public class AssistsRepository : BaseRepository<Assist>, IAssistsRepository
    {
        public AssistsRepository(AppDbContext context) : base(context)
        {
        }
    }
}

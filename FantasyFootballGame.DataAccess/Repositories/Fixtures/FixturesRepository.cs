using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.Fixtures
{
    public class FixturesRepository : BaseRepository<Fixture>, IFixturesRepository
    {
        public FixturesRepository(AppDbContext context) : base(context)
        {
        }
    }
}

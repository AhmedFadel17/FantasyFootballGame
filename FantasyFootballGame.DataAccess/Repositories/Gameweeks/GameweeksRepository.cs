using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Gameweeks
{
    public class GameweeksRepository : BaseRepository<Gameweek>, IGameweeksRepository
    {
        public GameweeksRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Gameweek> GetCurrentGameweek()
        {
            return await _dbSet
                .Where(gw => gw.Deadline >= DateTime.UtcNow)
                .OrderBy(gw => gw.Deadline)
                .FirstOrDefaultAsync();
        }
    }
}

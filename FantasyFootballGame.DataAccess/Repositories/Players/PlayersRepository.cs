using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.Players
{
    public class PlayersRepository : BaseRepository<Player>, IPlayersRepository
    {
        public PlayersRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Player>> GetByName(string name)
        {
            return await _dbSet.Where(p =>  p.Name == name).ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetByPrice(double min, double max)
        {
            return await _dbSet.Where(p => p.Price >= min && p.Price <= max).ToListAsync();
        }
    }
}

using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;
using FantasyFootballGame.DataAccess.Extentions.Players;
using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.DataAccess.Extentions.Common;

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

        public async Task<(IEnumerable<Player>,int)> GetAllWithPaginationAndFilters(int page, int pageSize,int? teamId,int? shirtNumber, string? name,PlayerStatus? status, PlayerPosition? position, double? minPrice, double? maxPrice)
        {
            var query = _dbSet.AsQueryable()
            .FilterByName(name)
            .FilterByPosition(position)
            .FilterByMinPrice(minPrice)
            .FilterByMaxPrice(maxPrice)
            .FilterByStatus(status)
            .FilterByTeam(teamId)
            .FilterByShirtNumber(shirtNumber);
            var totalCount=await query.CountAsync();
            var players=await query.Paginate(page, pageSize).ToListAsync();

            return (players,totalCount);
        }
    }
}

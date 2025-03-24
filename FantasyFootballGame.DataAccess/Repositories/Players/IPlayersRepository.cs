using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.Players
{
    public interface IPlayersRepository : IBaseRepository<Player>
    {
        public Task<IEnumerable<Player>> GetByName(string name);
        public Task<IEnumerable<Player>> GetByPrice(double min,double max);
        public Task<(IEnumerable<Player>,int)> GetAllWithPaginationAndFilters(int page, int pageSize,int? teamId,int? shirtNumber, string? name,PlayerStatus? status, PlayerPosition? position, double? minPrice, double? maxPrice);
    }
}

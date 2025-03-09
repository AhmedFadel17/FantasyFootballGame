using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.Players
{
    public interface IPlayersRepository : IBaseRepository<Player>
    {
        public Task<IEnumerable<Player>> GetByName(string name);
        public Task<IEnumerable<Player>> GetByPrice(double min,double max);

    }
}

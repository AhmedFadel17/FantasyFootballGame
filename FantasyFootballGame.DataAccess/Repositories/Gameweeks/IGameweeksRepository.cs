using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Repositories.Gameweeks
{
    public interface IGameweeksRepository : IBaseRepository<Gameweek>
    {
        Task<Gameweek> GetCurrentGameweek();
    }
}

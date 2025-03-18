using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.GameweekTeamPlayers
{
    public class GameweekTeamPlayersRepository : BaseRepository<GameweekTeamPlayer>, IGameweekTeamPlayersRepository
    {
        public GameweekTeamPlayersRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<GameweekTeamPlayer>> GetByTeamId(int teamId)
        {
            return await _dbSet.Where(p => p.GameweekTeamId == teamId).ToListAsync();

        }

        public async Task<GameweekTeamPlayer> GetPlayerFromTeam(int gameweekTeamId, int playerId)
        {
            return await _dbSet.Where(p=> p.GameweekTeamId==gameweekTeamId && p.PlayerId==playerId).FirstAsync();
        }
    }
}

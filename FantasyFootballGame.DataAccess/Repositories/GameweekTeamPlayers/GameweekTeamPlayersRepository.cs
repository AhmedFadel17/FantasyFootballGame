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

        public async Task<IEnumerable<GameweekTeamPlayer>> GetByUserId(int userId)
        {
            return await _dbSet
                .Include(p => p.FantasyTeamPlayer)
                    .ThenInclude(fp => fp.Player)
                .Where(p =>
                    p.GameweekTeam.FantasyTeam.UserId == userId &&
                    p.GameweekTeam.Gameweek.IsCurrent)
                .ToListAsync();
        }


        public async Task<GameweekTeamPlayer> GetPlayerFromTeam(int gameweekTeamId, int playerId)
        {
            return await _dbSet.Where(p=> p.GameweekTeamId==gameweekTeamId && p.PlayerId==playerId).FirstAsync();
        }
    }
}

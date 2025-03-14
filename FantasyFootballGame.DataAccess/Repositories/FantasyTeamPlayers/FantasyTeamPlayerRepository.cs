using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.FantasyTeamPlayers
{
    public class FantasyTeamPlayerRepository : BaseRepository<FantasyTeamPlayer>, IFanatsyTeamPlayersRepository
    {
        public FantasyTeamPlayerRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<FantasyTeamPlayer>> GetByTeam(int teamId)
        {
            return await _dbSet.Where(p => p.FantasyTeamId == teamId).ToListAsync();
        }

        public async Task<FantasyTeamPlayer> GetPlayerFromTeam(int teamId, int playerId)
        {
            return await _dbSet.Where(p => p.PlayerId == playerId && p.FantasyTeamId == teamId).FirstAsync();
        }
    }
}

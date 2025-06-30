using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.FantasyTeams
{
    public class FantasyTeamsRepository : BaseRepository<FantasyTeam>, IFantasyTeamsRepository
    {
        public FantasyTeamsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<FantasyTeam> GetByUserId(Guid userId)
        {
            return await _dbSet
        .Where(t => t.UserId == userId)
        .Include(t => t.Players)
            .ThenInclude(p => p.Player)
                .ThenInclude(pp => pp.Team)
        .FirstOrDefaultAsync();
        }
    }
}

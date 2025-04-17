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

        public async Task<FantasyTeam> GetByUserId(int userId)
        {
            return await _dbSet.Where(t => t.UserId==userId).FirstAsync();
        }
    }
}

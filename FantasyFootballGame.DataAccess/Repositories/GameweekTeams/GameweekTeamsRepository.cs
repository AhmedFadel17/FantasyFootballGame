using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.DataAccess.Repositories.GameweekTeams
{
    public class GameweekTeamsRepository : BaseRepository<GameweekTeam>, IGameweekTeamsRepository
    {
        public GameweekTeamsRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<GameweekTeam> GetCurrentGameweekTeam(int fantasyTeamId, int gameweekId)
        {
            return await _dbSet.Where(t => t.GameweekId == gameweekId && t.FantasyTeamId == fantasyTeamId).FirstAsync();
        }
    }
}

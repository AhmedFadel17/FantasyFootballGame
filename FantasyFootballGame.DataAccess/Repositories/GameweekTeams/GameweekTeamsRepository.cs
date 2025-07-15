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
            var gameweekTeam = await _dbSet
                .Where(t => t.GameweekId == gameweekId && t.FantasyTeamId == fantasyTeamId)
                .Include(t => t.Players)
                    .ThenInclude(p => p.Player)
                        .ThenInclude(p => p.Team)
                .FirstOrDefaultAsync();

            if (gameweekTeam == null)
                return null;

            var teamIds = gameweekTeam.Players
                .Select(p => p.Player.TeamId)
                .Distinct()
                .ToList();

            var fixtures = await _context.Fixtures
                .Where(f =>
                    f.GameweekId == gameweekId )
                    .Include(f => f.HomeTeam)
    .Include(f => f.AwayTeam)
                .ToListAsync();

            // Use LINQ to assign upcoming fixtures without manual foreach
            gameweekTeam.Players = gameweekTeam.Players
                .Select(p =>
                {
                    var teamId = p.Player.TeamId;
                    p.UpcomingFixture = fixtures.FirstOrDefault(f =>
                        (f.HomeTeamId == teamId || f.AwayTeamId == teamId));
                    return p;
                })
                .ToList();

            return gameweekTeam;
        }


        public async Task<bool> IsCurrentGameweekTeam(int gameweekTeamId)
        {
            return await _dbSet
                .AnyAsync(gt => gt.Id == gameweekTeamId &&
                                gt.Gameweek.IsCurrent);
        }
    }
}

using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.DataAccess.Extentions.Fixtures
{
    public static class FixturesExtensions
    {
        public static IQueryable<Fixture> FilterByTeam(this IQueryable<Fixture> query, int? teamId)
        {
            return teamId.HasValue ? query.Where(p => p.HomeTeamId == teamId.Value || p.AwayTeamId == teamId.Value) : query;
        }

        public static IQueryable<Fixture> FilterByGameweek(this IQueryable<Fixture> query, int? gameweekId)
        {
            return gameweekId.HasValue ? query.Where(p => p.GameweekId == gameweekId.Value) : query;
        }

        public static IQueryable<Fixture> FilterByPlayer(this IQueryable<Fixture> query, int? playerId)
        {
            if (!playerId.HasValue) return query;

            return query.Where(f =>
                f.HomeTeam.Players.Any(p => p.Id == playerId.Value) ||
                f.AwayTeam.Players.Any(p => p.Id == playerId.Value));
        }

        public static IQueryable<Fixture> FilterByDay(this IQueryable<Fixture> query, DateOnly? day)
        {
            return day.HasValue
                ? query.Where(p => p.MatchTime.Date == day.Value.ToDateTime(TimeOnly.MinValue).Date)
                : query;
        }
    }
}

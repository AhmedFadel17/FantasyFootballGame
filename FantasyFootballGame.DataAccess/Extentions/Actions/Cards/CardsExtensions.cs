using FantasyFootballGame.Domain.Models.Actions;

namespace FantasyFootballGame.DataAccess.Extentions.Actions.Cards
{
    public static class CardsExtensions
    {
        public static IQueryable<Card> FilterByTeam(this IQueryable<Card> query, int? teamId)
        {
            return teamId.HasValue ? query.Where(p => p.Player.TeamId == teamId.Value) : query;
        }

        public static IQueryable<Card> FilterByFixture(this IQueryable<Card> query, int? fixtureId)
        {
            return fixtureId.HasValue ? query.Where(p => p.FixtureId == fixtureId.Value) : query;
        }

        public static IQueryable<Card> FilterByGameweek(this IQueryable<Card> query, int? gameweekId)
        {
            return gameweekId.HasValue ? query.Where(p => p.Fixture.GameweekId == gameweekId.Value) : query;
        }

        public static IQueryable<Card> FilterByPlayer(this IQueryable<Card> query, int? playerId)
        {
            return playerId.HasValue ? query.Where(p => p.PlayerId == playerId.Value) : query;
        }
    }
}

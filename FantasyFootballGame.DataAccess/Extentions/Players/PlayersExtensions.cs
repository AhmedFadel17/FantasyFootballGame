using FantasyFootballGame.Domain.Models;
using FantasyFootballGame.Domain.Enums;
namespace FantasyFootballGame.DataAccess.Extentions.Players
{
    public static class PlayersExtensions
    {
        public static IQueryable<Player> FilterByName(this IQueryable<Player> query, string? name)
        {
            return string.IsNullOrEmpty(name) ? query : query.Where(p => p.Name.Contains(name));
        }

        public static IQueryable<Player> FilterByPosition(this IQueryable<Player> query, PlayerPosition? position)
        {
            return position.HasValue ? query.Where(p => p.Position == position.Value) : query;
        }

        public static IQueryable<Player> FilterByMinPrice(this IQueryable<Player> query, double? minPrice)
        {
            return minPrice.HasValue ? query.Where(p => p.Price >= minPrice.Value) : query;
        }

        public static IQueryable<Player> FilterByMaxPrice(this IQueryable<Player> query,double? maxPrice)
        {
            return maxPrice.HasValue ? query.Where(p => p.Price <= maxPrice.Value) : query;
        }

        public static IQueryable<Player> FilterByStatus(this IQueryable<Player> query, PlayerStatus? status)
        {
            return status.HasValue ? query.Where(p => p.Status == status.Value) : query;
        }

        public static IQueryable<Player> FilterByTeam(this IQueryable<Player> query, int? teamId)
        {
            return teamId.HasValue ? query.Where(p => p.TeamId == teamId.Value) : query;
        }

        public static IQueryable<Player> FilterByShirtNumber(this IQueryable<Player> query, int? shirtNumber)
        {
            return shirtNumber.HasValue ? query.Where(p => p.ShirtNumber == shirtNumber.Value) : query;
        }       


    }
}
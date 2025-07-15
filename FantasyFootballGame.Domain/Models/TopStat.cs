

namespace FantasyFootballGame.Domain.Models
{
    public record TopStat
    {
        public Player? Player { get; set; }
        public int? Stat { get; set; }
    }
}

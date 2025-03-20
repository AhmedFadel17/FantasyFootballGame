
using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models
{
    public record FantasyTeam
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int TotalPoints { get; set; } = 0;
        public double SquadValue { get; set; } = 100;
        public double InTheBank { get; set; } = 100;
        public int UserId { get; set; }

        [JsonIgnore]
        public IEnumerable<FantasyTeamPlayer>? Players { get; set; }
    }
}

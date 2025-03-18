using FantasyFootballGame.Domain.Models.Actions.Goals;
using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models.Actions.Penalties
{
    public record Penalty
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int Minute { get; set; }
        public int FixtureId { get; set; }
        public int? GoalId { get; set; }
        public bool IsScored { get; set; }

        [JsonIgnore]
        public Team Team { get; set; }
        [JsonIgnore]
        public Fixture Fixture { get; set; }
        [JsonIgnore]
        public Goal Goal { get; set; }
    }
}

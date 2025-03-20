using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models.Actions.Goals
{
    public record GoalScored
    {
        public int Id { get; set; }

        public int GoalId { get; set; }

        public int? PlayerId { get; set; }

        [JsonIgnore]
        public Goal? Goal { get; set; }

        [JsonIgnore]
        public Player? Player { get; set; }
    }
}

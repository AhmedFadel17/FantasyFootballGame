using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models.Actions
{
    public record Bonus
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int Points { get; set; }

        public int FixtureId { get; set; }

        [JsonIgnore]
        public Player? Player { get; set; }

        [JsonIgnore]
        public Fixture? Fixture { get; set; }
    }
}

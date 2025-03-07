using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models.Actions
{
    public record Goal
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public int Minute {  get; set; }

        public int FixtureId { get; set; }

        [JsonIgnore]
        public Team Team { get; set; }

        [JsonIgnore]
        public Fixture Fixture { get; set; }

    }
}

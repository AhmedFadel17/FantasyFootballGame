using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models
{
    public record Gameweek
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public int WeekNumber { get; set; }

        public int HighestPoints { get; set; }

        public DateTime Deadline { get; set; }

        public bool IsCurrent {  get; set; } = false;

        [JsonIgnore]
        public IEnumerable<Fixture>? Fixtures { get; set; }


    }
}

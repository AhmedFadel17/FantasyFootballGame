using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models
{
    public record Gameweek
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int WeekNumber { get; set; }

        public int HighestPoints { get; set; }

        public DateTime Deadline { get; set; }

        [JsonIgnore]
        public IEnumerable<Fixture>? Fixtures { get; set; }


    }
}

using FantasyFootballGame.Domain.Enums;
using System.Text.Json.Serialization;


namespace FantasyFootballGame.Domain.Models
{
    public record Player
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public double Price { get; set; }
        public int ShirtNumber { get; set; }
        public PlayerPosition Position { get; set; }
        public int TeamId { get; set; }

        [JsonIgnore]
        public Team Team { get; set; }
    }
}

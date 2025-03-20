using FantasyFootballGame.Domain.Enums;
using System.Text.Json.Serialization;


namespace FantasyFootballGame.Domain.Models
{
    public record Player
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string FullName { get; set; }
        public double Price { get; set; }
        public int ShirtNumber { get; set; }
        public PlayerPosition Position { get; set; }
        public int TeamId { get; set; }
        public PlayerStatus Status { get; set; } = PlayerStatus.Available;
        [JsonIgnore]
        public Team? Team { get; set; }
    }
}

using FantasyFootballGame.Domain.Enums.Players;
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
        public string ImageSrc { get; set; }
        public string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }

        [JsonIgnore]
        public Team? Team { get; set; }

        [JsonIgnore]
        public ICollection<PlayerStat>? PlayerStats { get; set; }

        [JsonIgnore]
        public ICollection<PlayerGameweekStats>? PlayerGameweekForms { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models
{
    public record GameweekTeamPlayer
    {
        public int Id { get; set; }
        public int GameweekTeamId { get; set; }
        public int PlayerId { get; set; }
        public int FantasyTeamPlayerId { get; set; }
        public bool IsStarting { get; set; }
        public bool IsCaptain { get; set; }
        public bool IsViceCaptain { get; set; }
        public int Points { get; set; }

        [JsonIgnore]
        public GameweekTeam? GameweekTeam { get; set; }

        [JsonIgnore]
        public Player? Player { get; set; }

        [JsonIgnore]
        public FantasyTeamPlayer? FantasyTeamPlayer { get; set; }
    }
}

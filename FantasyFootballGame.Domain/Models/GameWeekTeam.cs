using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models
{
    public record GameweekTeam
    {
        public int Id { get; set; }
        public int FantasyTeamId { get; set; }
        public int GameweekId { get; set; }
        public int TotalPoints { get; set; }
        public int TotalTransfers { get; set; }
        public int CaptainId { get; set; }
        public int ViceCaptainId { get; set; }

        [JsonIgnore]
        public FantasyTeam FantasyTeam { get; set; }

        [JsonIgnore]
        public Gameweek Gameweek { get; set; }

        [JsonIgnore]
        public Player Captain { get; set; }

        [JsonIgnore]
        public Player ViceCaptain { get; set; }

    }
}

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
        public int FreeTransfers { get; set; } = 1;
        public bool HasUnlimitedTransfers { get; set; } = false;
        public int UsedTransfers { get; set; } = 0;  
        public int TransferCost { get; set; } = 0;
        [JsonIgnore]
        public FantasyTeam? FantasyTeam { get; set; }

        [JsonIgnore]
        public Gameweek? Gameweek { get; set; }

    }
}

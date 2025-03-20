using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models.Actions
{
    public record Transfer
    {
        public int Id { get; set; }
        public int PlayerInId { get; set; }
        public int PlayerOutId { get; set; }
        public int FantasyTeamId { get; set; }
        public int GameweekId { get; set; }

        [JsonIgnore]
        public Player? PlayerIn { get; set; }
        [JsonIgnore]
        public Player? PlayerOut { get; set; }
        [JsonIgnore]
        public FantasyTeam? FantasyTeam { get; set; }
        [JsonIgnore]
        public Gameweek? Gameweek { get; set; }
    }
}

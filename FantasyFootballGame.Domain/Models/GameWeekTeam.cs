using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models
{
    public record GameWeekTeam
    {
        public int Id { get; set; }
        public int FantasyTeamId { get; set; }
        public int GameweekId { get; set; }
        public int TotalPoints { get; set; }
        public int TotalTransfers { get; set; }

        public int GkId { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int Player3Id { get; set; }
        public int Player4Id { get; set; }
        public int Player5Id { get; set; }
        public int Player6Id { get; set; }
        public int Player7Id { get; set; }
        public int Player8Id { get; set; }
        public int Player9Id { get; set; }
        public int Player10Id { get; set; }


        public int BenchGkId { get; set; }
        public int BenchPlayer1Id { get; set; }
        public int BenchPlayer2Id { get; set; }
        public int BenchPlayer3Id { get; set; }

        [JsonIgnore]
        public Player Gk { get; set; }
        [JsonIgnore]
        public Player BenchGk { get; set; }
        [JsonIgnore]
        public Player Player1 { get; set; }
        [JsonIgnore]
        public Player Player2 { get; set; }
        [JsonIgnore]
        public Player Player3 { get; set; }
        [JsonIgnore]
        public Player Player4 { get; set; }
        [JsonIgnore]
        public Player Player5 { get; set; }
        [JsonIgnore]
        public Player Player6 { get; set; }
        [JsonIgnore]
        public Player Player7 { get; set; }
        [JsonIgnore]
        public Player Player8 { get; set; }
        [JsonIgnore]
        public Player Player9 { get; set; }
        [JsonIgnore]
        public Player Player10 { get; set; }
        [JsonIgnore]
        public Player BenchPlayer1 { get; set; }
        [JsonIgnore]
        public Player BenchPlayer2 { get; set; }
        [JsonIgnore]
        public Player BenchPlayer3 { get; set; }
    }
}

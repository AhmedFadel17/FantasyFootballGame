using FantasyFootballGame.Domain.Enums;
using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models.Actions
{
    public record Injury
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public string Description { get; set; }
        public PlayerInjuryLevel Level { get; set; }
        public DateTime EndDate { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }
    }
}

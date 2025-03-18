using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models.Actions.Penalties
{
    public record PenaltyMiss
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int PenaltyId { get; set; }

        [JsonIgnore]
        public Penalty Penalty { get; set; }
        [JsonIgnore]
        public Player Player { get; set; }
    }
}

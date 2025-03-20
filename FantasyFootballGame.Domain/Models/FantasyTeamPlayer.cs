using FantasyFootballGame.Domain.Enums;
using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models
{
    public record FantasyTeamPlayer
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int FantasyTeamId { get; set; }
        public PlayerSlot Slot {  get; set; }
        
        [JsonIgnore]
        public Player? Player { get; set; }

        [JsonIgnore]
        public FantasyTeam? FantasyTeam { get; set; }
    }
}

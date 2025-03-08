using FantasyFootballGame.Domain.Enums;
using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models
{
    public record FantasyTeamPlayer
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int GameweekTeamId { get; set; }
        public bool IsBenched { get; set; }
        public PlayerSlot Slot {  get; set; }
        
        [JsonIgnore]
        public Player Player { get; set; }

        [JsonIgnore]
        public GameweekTeam GameweekTeam { get; set; }
    }
}

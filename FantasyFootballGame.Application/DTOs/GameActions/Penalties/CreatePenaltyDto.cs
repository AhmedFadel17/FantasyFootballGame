

namespace FantasyFootballGame.Application.DTOs.GameActions.Penalties
{
    public record CreatePenaltyDto
    {
        public int FixtureId { get; set; }
        public int TeamId { get; set; }
        public int Minute { get; set; }
        public int? GoalId { get; set; }
        public bool IsScored { get; set; }
        public int? MissPlayerId { get; set; }
        public int? SavePlayerId { get; set; }
    }
}

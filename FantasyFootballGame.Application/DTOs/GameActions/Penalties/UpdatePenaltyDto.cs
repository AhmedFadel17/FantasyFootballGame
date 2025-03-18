
namespace FantasyFootballGame.Application.DTOs.GameActions.Penalties
{
    public record UpdatePenaltyDto
    {
        public int? Minute { get; set; }
        public int? GoalId { get; set; }
        public bool? IsScored { get; set; }
    }
}

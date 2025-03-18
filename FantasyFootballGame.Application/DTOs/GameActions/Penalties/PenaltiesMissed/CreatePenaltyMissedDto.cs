
namespace FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed
{
    public record CreatePenaltyMissedDto
    {
        public int PlayerId { get; set; }
        public int PenaltyId { get; set; }
    }
}

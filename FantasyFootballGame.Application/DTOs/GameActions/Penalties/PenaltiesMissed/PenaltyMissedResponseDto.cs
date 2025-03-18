
namespace FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed
{
    public record PenaltyMissedResponseDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int PenaltyId { get; set; }
    }
}

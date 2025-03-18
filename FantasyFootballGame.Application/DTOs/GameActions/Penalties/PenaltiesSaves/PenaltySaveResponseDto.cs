
namespace FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves
{
    public record PenaltySaveResponseDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int PenaltyId { get; set; }
    }
}

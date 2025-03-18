
namespace FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves
{
    public record CreatePenaltySaveDto
    {
        public int PlayerId { get; set; }
        public int PenaltyId { get; set; }
    }
}

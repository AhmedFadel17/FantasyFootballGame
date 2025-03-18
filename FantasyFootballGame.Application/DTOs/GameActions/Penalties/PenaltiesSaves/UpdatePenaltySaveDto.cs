
namespace FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves
{
    public record UpdatePenaltySaveDto
    {
        public int? PlayerId { get; set; }
        public int? PenaltyId { get; set; }
    }
}

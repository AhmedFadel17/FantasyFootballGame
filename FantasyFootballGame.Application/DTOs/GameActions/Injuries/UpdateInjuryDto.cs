using FantasyFootballGame.Domain.Enums;

namespace FantasyFootballGame.Application.DTOs.GameActions.Injuries
{
    public record UpdateInjuryDto
    {
        public int? PlayerId { get; set; }
        public string? Description { get; set; }
        public PlayerInjuryLevel? Level { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsActive { get; set; }
    }
}



namespace FantasyFootballGame.Application.DTOs.GameActions.BonusPoints.IndvidualBonusPoints
{
    public record UpdateBonusPointDto
    {
        public int? PlayerId { get; set; }

        public int? Points { get; set; }
    }
}

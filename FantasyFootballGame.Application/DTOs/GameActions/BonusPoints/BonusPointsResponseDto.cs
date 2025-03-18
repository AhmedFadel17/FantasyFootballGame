
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints.IndvidualBonusPoints;

namespace FantasyFootballGame.Application.DTOs.GameActions.BonusPoints
{
    public record BonusPointsResponseDto
    {
        public int FixtureId { get; set; }

        public List<BonusPointResponseDto> BonusPoints { get; set;}
    }
}

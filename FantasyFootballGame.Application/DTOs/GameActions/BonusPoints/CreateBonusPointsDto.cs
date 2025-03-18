
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints.IndvidualBonusPoints;

namespace FantasyFootballGame.Application.DTOs.GameActions.BonusPoints
{
    public record CreateBonusPointsDto
    {
        public int FixtureId { get; set; }

        public List<CreateBonusPointDto> BonusPoints { get; set; }
    }
}

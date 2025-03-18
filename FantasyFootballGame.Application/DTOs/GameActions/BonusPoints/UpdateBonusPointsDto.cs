
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints.IndvidualBonusPoints;

namespace FantasyFootballGame.Application.DTOs.GameActions.BonusPoints
{
    public record UpdateBonusPointsDto
    {
        public List<CreateBonusPointDto> BonusPoints { get; set; }
    }
}

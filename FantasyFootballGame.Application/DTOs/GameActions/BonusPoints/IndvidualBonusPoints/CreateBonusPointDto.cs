
namespace FantasyFootballGame.Application.DTOs.GameActions.BonusPoints.IndvidualBonusPoints
{
    public record CreateBonusPointDto
    {
        public int PlayerId { get; set; }

        public int Points { get; set; }
    }
}

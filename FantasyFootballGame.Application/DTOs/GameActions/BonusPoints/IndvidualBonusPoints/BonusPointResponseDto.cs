
namespace FantasyFootballGame.Application.DTOs.GameActions.BonusPoints.IndvidualBonusPoints
{
    public record BonusPointResponseDto
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int Points { get; set; }
    }
}

using FantasyFootballGame.Domain.Enums.Cards;

namespace FantasyFootballGame.Application.DTOs.GameActions.Cards
{
    public record CardResponseDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int Minute { get; set; }
        public int FixtureId { get; set; }
        public CardType CardType { get; set; }
    }
}


namespace FantasyFootballGame.Application.DTOs.GameActions.Saves
{
    public record CreateSaveDto
    {
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int Minute { get; set; }
        public int FixtureId { get; set; }
    }
}


namespace FantasyFootballGame.Application.DTOs.Teams
{
    public record CreateTeamDto
    {
        public required string Name { get; set; }
        public required string Abbreviation { get; set; }
        public required string MainColor { get; set; }
        public required string SecondaryColor { get; set; }
        public required string ShirtImgSrc { get; set; }
    }
}

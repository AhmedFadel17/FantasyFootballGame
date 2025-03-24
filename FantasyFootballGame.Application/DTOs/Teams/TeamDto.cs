namespace FantasyFootballGame.Application.DTOs.Teams
{
    public record TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string MainColor { get; set; }
        public string SecondaryColor { get; set; }
        public string ShirtImgSrc { get; set; }
    }
} 
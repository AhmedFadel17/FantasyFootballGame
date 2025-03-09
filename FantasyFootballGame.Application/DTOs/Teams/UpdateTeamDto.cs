using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Teams
{
    public record UpdateTeamDto
    {
        [MaxLength(255)]
        public string? Name { get; set; }

        [MaxLength(4)]
        public string? Abbreviation { get; set; }

        [MaxLength(10)]
        public string? MainColor { get; set; }

        [MaxLength(10)]
        public string? SecondaryColor { get; set; }

        [MaxLength(455)]
        public string? ShirtImgSrc { get; set; }
    }
}

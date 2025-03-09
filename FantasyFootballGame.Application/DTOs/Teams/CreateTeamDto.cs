
using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Teams
{
    public record CreateTeamDto
    {
        [Required,MinLength(3),MaxLength(255)]
        public required string Name { get; set; }

        [Required, MinLength(3), MaxLength(4)]
        public required string Abbreviation { get; set; }

        [Required, MinLength(3), MaxLength(10)]
        public required string MainColor { get; set; }

        [Required, MinLength(3), MaxLength(10)]
        public required string SecondaryColor { get; set; }

        [Required, MaxLength(455)]
        public required string ShirtImgSrc { get; set; }
    }
}

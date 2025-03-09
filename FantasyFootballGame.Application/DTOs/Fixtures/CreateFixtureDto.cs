using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Fixtures
{
    public class CreateFixtureDto
    {
        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public int GameweekId { get; set; }

        [Required]
        public int HomeTeamScore { get; set; }

        [Required]
        public int AwayTeamScore { get; set; }

        [Required]
        public DateTime MatchTime { get; set; }

    }
}

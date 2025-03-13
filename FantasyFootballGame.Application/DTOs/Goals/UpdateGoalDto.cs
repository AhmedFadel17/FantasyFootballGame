using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Goals
{
    public record UpdateGoalDto
    {
        public int TeamId { get; set; }

        [Range(1, 90)]
        public int Minute { get; set; }

        public int FixtureId { get; set; }
    }
}

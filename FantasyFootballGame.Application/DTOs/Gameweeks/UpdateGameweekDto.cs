using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Gameweeks
{
    public class UpdateGameweekDto
    {
        [MinLength(3), MaxLength(255)]
        public required string Name { get; set; }

        [MaxLength(555)]
        public required string Description { get; set; }

        [Range(1, 99)]
        public int WeekNumber { get; set; }

        public int HighestPoints { get; set; }

        public DateTime Deadline { get; set; }
    }
}

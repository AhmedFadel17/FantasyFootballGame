using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Gameweeks
{
    public class CreateGameweekDto
    {
        [Required, MinLength(3), MaxLength(255)]
        public required string Name { get; set; }

        [Required, MaxLength(555)]
        public required string Description { get; set; }

        [Required, Range(1,99)]
        public int WeekNumber { get; set; }

        [Required]
        public int HighestPoints { get; set; }

        [Required]
        public DateTime Deadline { get; set; }
    }
}

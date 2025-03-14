using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Gameweeks
{
    public class UpdateGameweekDto
    {
        public string? Name { get; set; }

        public string? Description { get; set; }

        public int? WeekNumber { get; set; }

        public int? HighestPoints { get; set; }

        public DateTime? Deadline { get; set; }
    }
}

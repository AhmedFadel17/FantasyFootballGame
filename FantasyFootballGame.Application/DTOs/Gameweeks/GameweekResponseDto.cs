using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.DTOs.Gameweeks
{
    public class GameweekResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string? Description { get; set; }

        public int WeekNumber { get; set; }

        public int HighestPoints { get; set; }

        public DateTime Deadline { get; set; }

        public List<FixtureResponseDto> Fixtures { get; set; }
    }
}

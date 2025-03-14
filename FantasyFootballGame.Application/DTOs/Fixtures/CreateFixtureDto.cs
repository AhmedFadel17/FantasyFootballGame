using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Fixtures
{
    public class CreateFixtureDto
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int GameweekId { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public DateTime MatchTime { get; set; }

    }
}

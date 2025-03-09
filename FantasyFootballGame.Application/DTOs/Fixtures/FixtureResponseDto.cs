using FantasyFootballGame.Application.DTOs.Teams;

namespace FantasyFootballGame.Application.DTOs.Fixtures
{
    public class FixtureResponseDto
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int GameweekId { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public DateTime MatchTime { get; set; }

        public TeamResponseDto HomeTeam { get; set; }

        public TeamResponseDto AwayTeam { get; set; }
    }
}

using FantasyFootballGame.Domain.Enums.Fixtures;
using System.Text.Json.Serialization;

namespace FantasyFootballGame.Domain.Models
{
    public record Fixture
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int GameweekId { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public DateTime MatchTime { get; set; }

        public FixtureStatus Status { get; set; } = 0;

        [JsonIgnore]
        public Team HomeTeam { get; set; }

        [JsonIgnore]
        public Team AwayTeam { get; set; }

        [JsonIgnore]
        public Gameweek Gameweek { get; set; }

    }
}

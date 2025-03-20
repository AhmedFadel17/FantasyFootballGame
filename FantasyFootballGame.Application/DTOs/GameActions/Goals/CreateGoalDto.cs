
namespace FantasyFootballGame.Application.DTOs.GameActions.Goals
{
    public record CreateGoalDto
    {
        public int TeamId { get; set; }

        public int Minute { get; set; }

        public int FixtureId { get; set; }

        public int? GoalScorerId { get; set; }

        public int? OwnGoalScorerId { get; set; }

        public int? AssisterId { get; set; }
    }
}

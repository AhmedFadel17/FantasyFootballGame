using FantasyFootballGame.Application.DTOs.GameActions.Goals.Assists;
using FantasyFootballGame.Application.DTOs.GameActions.Goals.GoalsScored;
using FantasyFootballGame.Application.DTOs.GameActions.Goals.OwnGoals;

namespace FantasyFootballGame.Application.DTOs.GameActions.Goals
{
    public record GoalResponseDto
    {
        public int Id { get; set; }

        public int TeamId { get; set; }

        public int Minute { get; set; }

        public int FixtureId { get; set; }

        public GoalScoredResponseDto? GoalScored { get; set; }

        public AssistResponseDto? Assist { get; set; }

        public OwnGoalResponseDto? OwnGoal { get; set; }
    }
}

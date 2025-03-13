using FantasyFootballGame.Application.DTOs.Goals.Assists;
using FantasyFootballGame.Application.DTOs.Goals.GoalsScored;
using FantasyFootballGame.Application.DTOs.Goals.OwnGoals;

namespace FantasyFootballGame.Application.DTOs.Goals
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

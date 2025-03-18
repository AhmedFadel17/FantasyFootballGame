namespace FantasyFootballGame.Application.DTOs.GameActions.Goals.GoalsScored
{
    public record CreateGoalScoredDto
    {
        public int GoalId { get; set; }

        public int? PlayerId { get; set; }
    }
}

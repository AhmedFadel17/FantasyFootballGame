using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Goals.GoalsScored
{
    public record CreateGoalScoredDto
    {
        public int GoalId { get; set; }

        public int? PlayerId { get; set; }
    }
}

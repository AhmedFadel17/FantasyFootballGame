using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Goals.OwnGoals
{
    public record CreateOwnGoalDto
    {
        public int GoalId { get; set; }

        public int? PlayerId { get; set; }

    }
}

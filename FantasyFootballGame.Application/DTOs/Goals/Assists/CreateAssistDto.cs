using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Goals.Assists
{
    public record CreateAssistDto
    {
        public int GoalId { get; set; }

        public int? PlayerId { get; set; }
    }
}

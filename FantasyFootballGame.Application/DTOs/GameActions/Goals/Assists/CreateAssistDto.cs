namespace FantasyFootballGame.Application.DTOs.GameActions.Goals.Assists
{
    public record CreateAssistDto
    {
        public int GoalId { get; set; }

        public int? PlayerId { get; set; }
    }
}

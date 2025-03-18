using FantasyFootballGame.Application.DTOs.Players;

namespace FantasyFootballGame.Application.DTOs.GameActions.Goals.GoalsScored
{
    public record GoalScoredResponseDto
    {
        public int Id { get; set; }

        public int GoalId { get; set; }

        public int? PlayerId { get; set; }

        public PlayerResponseDto? Player { get; set; }
    }
}

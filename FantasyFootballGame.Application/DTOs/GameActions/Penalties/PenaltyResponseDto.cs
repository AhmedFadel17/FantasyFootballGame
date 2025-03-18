
using FantasyFootballGame.Application.DTOs.GameActions.Goals;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves;

namespace FantasyFootballGame.Application.DTOs.GameActions.Penalties
{
    public record PenaltyResponseDto
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int Minute { get; set; }
        public int FixtureId { get; set; }
        public int? GoalId { get; set; }
        public bool IsScored { get; set; }
        public GoalResponseDto? Goal { get; set; }
        public PenaltySaveResponseDto? PenaltySave { get; set; }
        public PenaltyMissedResponseDto? PenaltyMiss { get; set; }

    }
}

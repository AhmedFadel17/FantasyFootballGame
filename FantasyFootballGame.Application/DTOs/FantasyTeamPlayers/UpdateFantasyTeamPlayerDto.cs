using FantasyFootballGame.Domain.Enums;

namespace FantasyFootballGame.Application.DTOs.FantasyTeamPlayers
{
    public class UpdateFantasyTeamPlayerDto
    {
        public int PlayerId { get; set; }

        public bool IsBenched { get; set; }

        public PlayerSlot Slot { get; set; }
    }
}

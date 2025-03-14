using FantasyFootballGame.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.FantasyTeamPlayers
{
    public class CreateFantasyTeamPlayerDto
    {
        public int PlayerId { get; set; }
        public PlayerSlot Slot { get; set; }
    }
}

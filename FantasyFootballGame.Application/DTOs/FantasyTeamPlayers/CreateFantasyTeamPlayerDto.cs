using FantasyFootballGame.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.FantasyTeamPlayers
{
    public class CreateFantasyTeamPlayerDto
    {
        [Required]
        public int PlayerId { get; set; }

        //[Required]
        //public int GameweekTeamId { get; set; }

        [Required]
        public bool IsBenched { get; set; }

        [Required,EnumDataType(typeof(PlayerSlot))]
        public PlayerSlot Slot { get; set; }

    }
}

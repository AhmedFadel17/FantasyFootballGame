using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.FantasyTeams
{
    public class CreateFantasyTeamDto
    {
        [Required, MinLength(3), MaxLength(255)]
        public required string Name { get; set; }
        public int UserId { get; set; }
        public List<CreateFantasyTeamPlayerDto> Players { get; set; }
    }
}

using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Domain.Enums;

namespace FantasyFootballGame.Application.DTOs.FantasyTeamPlayers
{
    public class FantasyTeamPlayerResponseDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int FantasyTeamId { get; set; }
        public PlayerResponseDto Player { get; set; }
    }
}

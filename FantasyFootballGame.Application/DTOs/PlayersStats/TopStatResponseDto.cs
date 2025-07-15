using FantasyFootballGame.Application.DTOs.Players;


namespace FantasyFootballGame.Application.DTOs.PlayersStats
{
    public record TopStatResponseDto
    {
        public PlayerResponseDto? Player { get; set; }
        public int? Stat { get; set; }
    }
}

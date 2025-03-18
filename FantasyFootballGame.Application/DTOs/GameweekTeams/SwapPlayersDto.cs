using FantasyFootballGame.Application.DTOs.Swaps;

namespace FantasyFootballGame.Application.DTOs.GameweekTeams
{
    public record SwapPlayersDto
    {
        public required int GameweekTeamId { get; set; }
        public required List<CreateSwapDto> Swaps { get; set; }
    }
}

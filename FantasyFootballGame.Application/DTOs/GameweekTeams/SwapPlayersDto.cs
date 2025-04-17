using FantasyFootballGame.Application.DTOs.Swaps;

namespace FantasyFootballGame.Application.DTOs.GameweekTeams
{
    public record SwapPlayersDto
    {
        public required List<CreateSwapDto> Swaps { get; set; }
    }
}

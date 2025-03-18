
namespace FantasyFootballGame.Application.DTOs.Swaps
{
    public record CreateSwapDto
    {
        public int PlayerInId { get; set; }
        public int PlayerOutId { get; set; }
    }
}

using FantasyFootballGame.Domain.Enums;

namespace FantasyFootballGame.Application.DTOs.Players
{
    public record UpdatePlayerDto
    {
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public double? Price { get; set; }
        public int? ShirtNumber { get; set; }
        public PlayerPosition? Position { get; set; }
        public PlayerStatus? Status { get; set; }
        public int? TeamId { get; set; }
    }
}

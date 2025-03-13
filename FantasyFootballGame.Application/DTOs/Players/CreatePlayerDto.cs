using FantasyFootballGame.Domain.Enums;

namespace FantasyFootballGame.Application.DTOs.Players
{
    public record CreatePlayerDto
    {
        public required string Name { get; set; }

        public required string FullName { get; set; }

        public double Price { get; set; }

        public int ShirtNumber { get; set; }

        public PlayerPosition Position { get; set; }

        public int TeamId { get; set; }

    }
}

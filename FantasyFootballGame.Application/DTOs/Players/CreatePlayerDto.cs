using FantasyFootballGame.Domain.Enums;

namespace FantasyFootballGame.Application.DTOs.Players
{
    public record CreatePlayerDto
    {
        public required string Name { get; set; }

        public required string FullName { get; set; }

        public double Price { get; set; }

        public int ShirtNumber { get; set; }

        public required string Position { get; set; }

        public int TeamId { get; set; }

        public required string ImageSrc { get; set; }
        public required string Country { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }

    }
}

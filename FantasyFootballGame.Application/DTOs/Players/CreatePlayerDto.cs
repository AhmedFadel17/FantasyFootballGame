using FantasyFootballGame.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Players
{
    public record CreatePlayerDto
    {
        [Required,MinLength(3),MaxLength(255)]
        public required string Name { get; set; }

        [Required, MinLength(3), MaxLength(255)]
        public required string FullName { get; set; }

        [Required,Range(1,99)]
        public double Price { get; set; }

        [Required,Range(1,99)]
        public int ShirtNumber { get; set; }

        [Required, EnumDataType(typeof(PlayerPosition))]
        public PlayerPosition Position { get; set; }

        [Required]
        public int TeamId { get; set; }

    }
}

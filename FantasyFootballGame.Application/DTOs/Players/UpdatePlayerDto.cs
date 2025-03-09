using FantasyFootballGame.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Players
{
    public record UpdatePlayerDto
    {
        [MinLength(3),MaxLength(255)]
        public string Name { get; set; }

        [MinLength(3), MaxLength(255)]
        public string FullName { get; set; }

        [Range(1, 99)]
        public double Price { get; set; }

        [Range(1, 99)]
        public int ShirtNumber { get; set; }

        public PlayerPosition Position { get; set; }

        public PlayerStatus Status { get; set; }

        public int TeamId { get; set; }
    }
}

using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Players
{
    public record PlayerResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public double Price { get; set; }
        public int ShirtNumber { get; set; }
        public PlayerPosition Position { get; set; }
        public PlayerStatus Status { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}

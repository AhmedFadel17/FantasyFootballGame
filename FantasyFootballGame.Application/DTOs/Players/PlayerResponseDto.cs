using FantasyFootballGame.Application.DTOs.Teams;
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
        public string Position { get; set; }
        public string Status { get; set; }
        public int TeamId { get; set; }
        //public TeamResponseDto? Team { get; set; }
    }
}

using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.DTOs.Players
{
    public class UpdatePlayerDto 
    {
        public string? Name { get; set; }
        public string? FullName { get; set; }
        public double? Price { get; set; }
        public int? ShirtNumber { get; set; }
        public string? Position { get; set; }
        public string? Status { get; set; }
        public int? TeamId { get; set; }
    }
}

using FantasyFootballGame.Application.DTOs.Teams;

namespace FantasyFootballGame.Application.DTOs.Players
{
    public record PlayerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public double Price { get; set; }
        public int ShirtNumber { get; set; }
        public string Position { get; set; }
        public string Status { get; set; }
        public int TeamId { get; set; }
        public TeamDto? Team { get; set; }
    }
} 
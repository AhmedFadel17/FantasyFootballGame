using FantasyFootballGame.Application.DTOs.GameweekTeamPlayers;


namespace FantasyFootballGame.Application.DTOs.GameweekTeams
{
    public record GameweekTeamResponseDto
    {
        public int Id { get; set; }
        public int FantasyTeamId { get; set; }
        public int GameweekId { get; set; }
        public int TotalPoints { get; set; }
        public int Chip { get; set; }
        public int TransfersMade { get; set; } 
        public int TransferCost { get; set; } 
        public List<GameweekTeamPlayerResponseDto>? Players { get; set; }
        public List<GameweekTeamPlayerResponseDto>? Starters { get; set; }
        public List<GameweekTeamPlayerResponseDto>? Benched { get; set; }

    }
}

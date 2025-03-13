using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;

namespace FantasyFootballGame.Application.DTOs.FantasyTeams
{
    public class FantasyTeamResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public double SquadValue { get; set; }
        public double InTheBank { get; set; }
        public int UserId { get; set; }
        public List<FantasyTeamPlayerResponseDto> Players { get; set; }
    }
}

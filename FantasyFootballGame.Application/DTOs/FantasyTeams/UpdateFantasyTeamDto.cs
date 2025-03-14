using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.FantasyTeams
{
    public class UpdateFantasyTeamDto
    {
        public string? Name { get; set; }
        public int? TotalPoints { get; set; }
        public double? SquadValue { get; set; }
        public double? InTheBank { get; set; }
    }
}

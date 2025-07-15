using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.Domain.Models;
using System.Text.Json.Serialization;

namespace FantasyFootballGame.Application.DTOs.PlayersStats
{
    public class PlayerStatsResponseDto
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int SeasonNumber { get; set; }
        public int MatchesPlayed { get; set; }
        public int MatchesStarted { get; set; }
        public int MinutesPlayed { get; set; }
        public int GoalsScored { get; set; }
        public int Assists { get; set; }
        public int OwnGoals { get; set; }
        public int CleanSheets { get; set; }
        public int Saves { get; set; }
        public int PenaltyMisses { get; set; }
        public int PenaltySaved { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int BonusPoints { get; set; }
        public int TotalPoints { get; set; }
    }
} 
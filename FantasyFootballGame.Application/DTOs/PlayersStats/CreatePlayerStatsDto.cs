namespace FantasyFootballGame.Application.DTOs.PlayersStats
{
    public class CreatePlayerStatsDto
    {
        public int PlayerId { get; set; }
        public int GameweekId { get; set; }
        public int TotalPoints { get; set; }
        public int GoalsScored { get; set; }
        public int Assists { get; set; }
        public int CleanSheets { get; set; }
        public int OwnGoals { get; set; }
        public int PenaltiesSaved { get; set; }
        public int PenaltiesMissed { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int Saves { get; set; }
        public int BonusPoints { get; set; }
    }
} 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FantasyFootballGame.Domain.Models
{
    public record PlayerGameweekStats
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int GameweekId { get; set; }

        public int FixtureId { get; set; }

        public bool IsStarting { get; set; }
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
        [JsonIgnore]
        public Player Player { get; set; }

        [JsonIgnore]
        public Fixture Fixture { get; set; }

        [JsonIgnore]
        public Gameweek Gameweek { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FantasyFootballGame.Domain.Models
{
    public record PlayerGameweekForm
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int FixtureId { get; set; }

        public bool IsStarting { get; set; }

        public int MinutesPlayed { get; set; }

        public int TotalPoints { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }

        [JsonIgnore]
        public Fixture Fixture { get; set; }
    }
}

using FantasyFootballGame.Domain.Enums.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FantasyFootballGame.Domain.Models.Actions
{
    public record Card
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int TeamId { get; set; }
        public int Minute { get; set; }
        public int FixtureId { get; set; }
        public CardType CardType { get; set; }
        [JsonIgnore]
        public Player? Player { get; set; }
        [JsonIgnore]
        public Team? Team { get; set; }
        [JsonIgnore]
        public Fixture? Fixture { get; set; }
    }
}

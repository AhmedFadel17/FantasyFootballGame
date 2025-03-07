using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FantasyFootballGame.Domain.Models.Actions
{
    public record Assist
    {
        public int Id { get; set; }

        public int GoalId { get; set; }

        public int? PlayerId { get; set; }

        [JsonIgnore]
        public Goal Goal { get; set; }

        [JsonIgnore]
        public Player Player { get; set; }
    }
}

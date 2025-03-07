using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FantasyFootballGame.Domain.Models
{
    public record FantasyTeam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TotalPoints { get; set; }
        public double SquadValue { get; set; }
        public double InTheBank { get; set; }
        public int UserId { get; set; }

        public int Gk1Id { get; set; }
        public int Gk2Id { get;set; }
        public int Def1Id { get; set; }
        public int Def2Id { get; set; }
        public int Def3Id { get; set; }
        public int Def4Id { get; set; }
        public int Def5Id { get; set; }
        public int Mid1Id { get; set; }
        public int Mid2Id { get; set; }
        public int Mid3Id { get; set; }
        public int Mid4Id { get; set; }
        public int Mid5Id { get; set; }
        public int Frw1Id { get; set; }
        public int Frw2Id { get; set; }
        public int Frw3Id { get; set; }

        [JsonIgnore]
        public Player Gk1 { get; set; }
        [JsonIgnore]
        public Player Gk2 { get; set; }
        [JsonIgnore]
        public Player Def1 { get; set; }
        [JsonIgnore]
        public Player Def2 { get; set; }
        [JsonIgnore]
        public Player Def3 { get; set; }
        [JsonIgnore]
        public Player Def4 { get; set; }
        [JsonIgnore]
        public Player Def5 { get; set; }
        [JsonIgnore]
        public Player Mid1 { get; set; }
        [JsonIgnore]
        public Player Mid2 { get; set; }
        [JsonIgnore]
        public Player Mid3 { get; set; }
        [JsonIgnore]
        public Player Mid4 { get; set; }
        [JsonIgnore]
        public Player Mid5 { get; set; }
        [JsonIgnore]
        public Player Frw1 { get; set; }
        [JsonIgnore]
        public Player Frw2 { get; set; }
        [JsonIgnore]
        public Player Frw3 { get; set; }

    }
}

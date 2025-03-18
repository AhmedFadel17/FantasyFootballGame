using FantasyFootballGame.Application.DTOs.Goals.Assists;
using FantasyFootballGame.Application.DTOs.Goals.GoalsScored;
using FantasyFootballGame.Application.DTOs.Goals.OwnGoals;
using FantasyFootballGame.Domain.Models;
using System;
using System.Collections.Generic;

namespace FantasyFootballGame.Application.DTOs.GameActions.Goals
{
    public record CreateGoalDto
    {
        public int TeamId { get; set; }

        public int Minute { get; set; }

        public int FixtureId { get; set; }

        public int? GoalScorerId { get; set; }

        public int? OwnGoalScorerId { get; set; }

        public int? AssisterId { get; set; }
    }
}

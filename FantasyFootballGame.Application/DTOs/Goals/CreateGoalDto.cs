using FantasyFootballGame.Application.DTOs.Goals.Assists;
using FantasyFootballGame.Application.DTOs.Goals.GoalsScored;
using FantasyFootballGame.Application.DTOs.Goals.OwnGoals;
using FantasyFootballGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FantasyFootballGame.Application.DTOs.Goals
{
    public record CreateGoalDto
    {
        [Required]
        public int TeamId { get; set; }

        [Required,Range(1,90)]
        public int Minute { get; set; }

        [Required]
        public int FixtureId { get; set; }

        public CreateGoalScoredDto? GoalScored { get; set; }

        public CreateOwnGoalDto? OwnGoal { get; set; }

        public CreateAssistDto? Assist { get; set; }
    }
}

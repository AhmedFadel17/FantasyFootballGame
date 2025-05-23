﻿using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;

namespace FantasyFootballGame.Application.DTOs.FantasyTeams
{
    public class CreateFantasyTeamDto
    {
        public string Name { get; set; } = string.Empty;
        public List<CreateFantasyTeamPlayerDto> Players { get; set; }
    }
}

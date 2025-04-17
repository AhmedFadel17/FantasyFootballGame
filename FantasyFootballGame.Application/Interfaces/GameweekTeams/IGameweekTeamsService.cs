using FantasyFootballGame.Application.DTOs.GameweekTeams;
using FantasyFootballGame.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballGame.Application.Interfaces.GameweekTeams
{
    public interface IGameweekTeamsService
    {
        Task<GameweekTeam> Create(int fantasyTeamId);
        Task Swap(int userId,SwapPlayersDto dto);


    }
}

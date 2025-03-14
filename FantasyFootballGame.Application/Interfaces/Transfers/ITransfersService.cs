using FantasyFootballGame.Application.DTOs.FantasyTeams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasyFootballGame.Application.Interfaces.Transfers
{
    public interface ITransfersService
    {
        Task Create(MakeTransfersDto dto);
    }
}

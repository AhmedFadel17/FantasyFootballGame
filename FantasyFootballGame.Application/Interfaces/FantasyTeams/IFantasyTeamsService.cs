using FantasyFootballGame.Application.DTOs.FantasyTeams;

namespace FantasyFootballGame.Application.Interfaces.FantasyTeams
{
    public interface IFantasyTeamsService
    {
        //Task<List<PlayerResponseDto>> All();
        Task<FantasyTeamResponseDto> GetById(int id);
        Task<FantasyTeamResponseDto> Update(int id, UpdateFantasyTeamDto dto);
        Task<FantasyTeamResponseDto> Create(CreateFantasyTeamDto dto);
        Task Delete(int id);
        Task MakeTransfers(MakeTransfersDto dto);
    }
}

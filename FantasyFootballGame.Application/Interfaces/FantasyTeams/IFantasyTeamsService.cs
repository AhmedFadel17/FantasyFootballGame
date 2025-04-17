using FantasyFootballGame.Application.DTOs.FantasyTeams;

namespace FantasyFootballGame.Application.Interfaces.FantasyTeams
{
    public interface IFantasyTeamsService
    {
        //Task<List<PlayerResponseDto>> All();
        Task<FantasyTeamResponseDto> GetById(int id);
        Task<FantasyTeamResponseDto> Update(int id, UpdateFantasyTeamDto dto);
        Task<FantasyTeamResponseDto> Create(int userId,CreateFantasyTeamDto dto);
        Task Delete(int id);

        Task<FantasyTeamResponseDto> GetByUserId(int id);
        Task DeleteByUserId(int id);

    }
}

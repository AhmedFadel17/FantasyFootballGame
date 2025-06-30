using FantasyFootballGame.Application.DTOs.FantasyTeams;

namespace FantasyFootballGame.Application.Interfaces.FantasyTeams
{
    public interface IFantasyTeamsService
    {
        //Task<List<PlayerResponseDto>> All();
        Task<FantasyTeamResponseDto> GetById(int id);
        Task<FantasyTeamResponseDto> Update(Guid userId, UpdateFantasyTeamDto dto);
        Task<FantasyTeamResponseDto> Create(Guid userId,CreateFantasyTeamDto dto);
        Task Delete(int id);

        Task<FantasyTeamResponseDto> GetByUserId(Guid id);
        Task DeleteByUserId(Guid id);

    }
}

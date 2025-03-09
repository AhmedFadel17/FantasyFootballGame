using FantasyFootballGame.Application.DTOs.Teams;

namespace FantasyFootballGame.Application.Interfaces.Teams
{
    public interface ITeamsService
    {
        Task<List<TeamResponseDto>> All();
        Task<TeamResponseDto> GetById(int id);
        Task<TeamResponseDto> Update(int id,UpdateTeamDto dto);
        Task<TeamResponseDto> Create(CreateTeamDto dto);
        Task Delete(int id);
    }
}

using AutoMapper;
using FantasyFootballGame.Application.DTOs.Teams;
using FantasyFootballGame.Application.Interfaces.Teams;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.Services.Teams
{
    public class TeamsService : ITeamsService
    {
        private readonly ITeamsRepository _repo;
        private readonly IMapper _mapper;
        public TeamsService(ITeamsRepository repository,IMapper mapper)
        {
            _mapper = mapper;
            _repo = repository;
        }
        public async Task<List<TeamResponseDto>> All()
        {
            var teams= await _repo.GetAll();
            return _mapper.Map<List<TeamResponseDto>>(teams);  
        }

        public async Task<TeamResponseDto> Create(CreateTeamDto dto)
        {
            var team= _mapper.Map<Team>(dto);
            await _repo.Create(team);
            await _repo.Save();
            return _mapper.Map<TeamResponseDto>(team);
        }

        public async Task Delete(int id)
        {
            var team = await _repo.GetById(id);
            if(team == null) throw new KeyNotFoundException("Team is not found");
            _repo.Delete(team);
            await _repo.Save();
        }

        public async Task<TeamResponseDto> GetById(int id)
        {
            var team = await _repo.GetById(id);
            if (team == null) throw new KeyNotFoundException("Team is not found");
            return _mapper.Map<TeamResponseDto>(team);
        }

        public async Task<TeamResponseDto> Update(int id, UpdateTeamDto dto)
        {
            var team = await _repo.GetById(id);
            if (team == null) throw new KeyNotFoundException("Team is not found");
            var updatedTeam= _mapper.Map<Team>(dto);
            _repo.Update(updatedTeam);
            await _repo.Save();
            return _mapper.Map<TeamResponseDto>(updatedTeam);
        }
    }
}

using AutoMapper;
using FantasyFootballGame.Application.DTOs.Teams;
using FantasyFootballGame.Application.Interfaces.Teams;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FantasyFootballGame.Domain.Models;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.Teams
{
    public class TeamsService : ITeamsService
    {
        private readonly ITeamsRepository _teamsRepo;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateTeamDto> _createValidator;
        private readonly IValidator<UpdateTeamDto> _updateValidator;

        public TeamsService(
            ITeamsRepository teamsRepo,
            IMapper mapper,
            IValidator<CreateTeamDto> createValidator,
            IValidator<UpdateTeamDto> updateValidator)
        {
            _teamsRepo = teamsRepo;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<List<TeamResponseDto>> All()
        {
            var teams = await _teamsRepo.GetAll();
            return _mapper.Map<List<TeamResponseDto>>(teams);
        }

        public async Task<TeamResponseDto> Create(CreateTeamDto dto)
        {
            await _createValidator.ValidateAndThrowAsync(dto);
            var team = _mapper.Map<Team>(dto);
            await _teamsRepo.Create(team);
            await _teamsRepo.Save();
            return _mapper.Map<TeamResponseDto>(team);
        }

        public async Task Delete(int id)
        {
            var team = await _teamsRepo.GetById(id);
            if (team == null)
                throw new Exception($"Team with id {id} not found");
            _teamsRepo.Delete(team);
            await _teamsRepo.Save();
        }

        public async Task<TeamResponseDto> GetById(int id)
        {
            var team = await _teamsRepo.GetById(id);
            if (team == null)
                throw new Exception($"Team with id {id} not found");
            return _mapper.Map<TeamResponseDto>(team);
        }

        public async Task<TeamResponseDto> Update(int id, UpdateTeamDto dto)
        {
            await _updateValidator.ValidateAndThrowAsync(dto);
            var team = await _teamsRepo.GetById(id);
            if (team == null)
                throw new Exception($"Team with id {id} not found");
            _mapper.Map(dto, team);
            _teamsRepo.Update(team);
            await _teamsRepo.Save();
            return _mapper.Map<TeamResponseDto>(team);
        }
    }
}

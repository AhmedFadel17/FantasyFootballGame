using AutoMapper;
using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.Interfaces.FantasyTeams;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeams;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.Services.FantasyTeams
{
    public class FantasyTeamsService : IFantasyTeamsService
    {
        private readonly IFantasyTeamsRepository _teamsRepo;
        private readonly IFanatsyTeamPlayersRepository _playersRepo;
        private readonly IMapper _mapper;
        public FantasyTeamsService(IFantasyTeamsRepository teamsRepository,IFanatsyTeamPlayersRepository playersRepository,IMapper mapper)
        {
            _mapper = mapper;
            _teamsRepo = teamsRepository; 
            _playersRepo = playersRepository;
        }

        public async Task<FantasyTeamResponseDto> Create(CreateFantasyTeamDto dto)
        {
            var pCount = 15;
            var players = dto.Players;
            if (players.Count != 15) throw new ArgumentException($"Players must be {pCount}");
            var team = _mapper.Map<FantasyTeam>(dto);
            await _teamsRepo.Create(team);
            await _teamsRepo.Save();
            foreach (var playerDto in players)
            {
                var player = _mapper.Map<FantasyTeamPlayer>((team.Id,playerDto));
                await _playersRepo.Create(player);
            }
            await _playersRepo.Save();
            return _mapper.Map<FantasyTeamResponseDto>(team);
        }

        public async Task Delete(int id)
        {
            var team = await _teamsRepo.GetById(id);
            if (team == null) throw new KeyNotFoundException("Fantasy team not found");
            _teamsRepo.Delete(team);
            await _teamsRepo.Save();
        }

        public async Task<FantasyTeamResponseDto> GetById(int id)
        {
            var team = await _teamsRepo.GetById(id);
            if (team == null) throw new KeyNotFoundException("Fantasy team not found");
            return _mapper.Map<FantasyTeamResponseDto>(team);
        }

        public Task MakeTransfers(MakeTransfersDto dto)
        {
            throw new NotImplementedException();
        }

        public async Task<FantasyTeamResponseDto> Update(int id, UpdateFantasyTeamDto dto)
        {
            var team = await _teamsRepo.GetById(id);
            if (team == null) throw new KeyNotFoundException("Fantasy team not found");
            var updatedTeam= _mapper.Map<FantasyTeam>(team);
            _teamsRepo.Update(updatedTeam);
            await _teamsRepo.Save();
            return _mapper.Map<FantasyTeamResponseDto>(updatedTeam);
        }
    }
}

using AutoMapper;
using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;
using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.Interfaces.FantasyTeams;
using FantasyFootballGame.Application.Interfaces.GameweekTeams;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeams;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.Services.FantasyTeams
{
    public class FantasyTeamsService : IFantasyTeamsService
    {
        private readonly IFantasyTeamsRepository _teamsRepo;
        private readonly IFanatsyTeamPlayersRepository _fantasyPlayersRepo;
        private readonly IPlayersRepository _playersRepo;
        private readonly IMapper _mapper;
        private readonly IGameweekTeamsService _gameweekTeamsService;
        public FantasyTeamsService(
            IFantasyTeamsRepository teamsRepository,
            IFanatsyTeamPlayersRepository fantasyPlayerRepo,
            IPlayersRepository playersRepository,
            IGameweekTeamsService gameweekTeamsService,
            IMapper mapper)
        {
            _mapper = mapper;
            _teamsRepo = teamsRepository; 
            _fantasyPlayersRepo = fantasyPlayerRepo;
            _playersRepo = playersRepository;
            _gameweekTeamsService = gameweekTeamsService;
        }

        public async Task<FantasyTeamResponseDto> Create(CreateFantasyTeamDto dto)
        {
            var players = dto.Players;
            var teamValue=await CalculateTeamValue(players);
            var team = _mapper.Map<FantasyTeam>((teamValue.squadValue,teamValue.inTheBank,dto));

            await _teamsRepo.Create(team);
            await _teamsRepo.Save();

            foreach (var playerDto in players)
            {
                var player = _mapper.Map<FantasyTeamPlayer>((team.Id, playerDto));
                await _fantasyPlayersRepo.Create(player);
            }

            await _fantasyPlayersRepo.Save();
            await _gameweekTeamsService.Create(team.Id);
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

        

        public async Task<FantasyTeamResponseDto> Update(int id, UpdateFantasyTeamDto dto)
        {
            var team = await _teamsRepo.GetById(id);
            if (team == null) throw new KeyNotFoundException("Fantasy team not found");
            var updatedTeam= _mapper.Map<FantasyTeam>(team);
            _teamsRepo.Update(updatedTeam);
            await _teamsRepo.Save();
            return _mapper.Map<FantasyTeamResponseDto>(updatedTeam);
        }

        private async Task<(double squadValue, double inTheBank)> CalculateTeamValue(List<CreateFantasyTeamPlayerDto> players)
        {
            var budget = 100;
            var playerIds = players.Select(p => p.PlayerId).ToList();
            var playersData = await _playersRepo.GetByIds(playerIds);
            double squadValue = playersData.Sum(p => p.Price);
            double inTheBank = budget - squadValue;
            return (squadValue, inTheBank);
        }

    }
}

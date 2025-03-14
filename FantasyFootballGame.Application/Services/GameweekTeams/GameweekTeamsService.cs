using FantasyFootballGame.Application.Interfaces.GameweekTeams;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeams;
using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.Domain.Models;

namespace FantasyFootballGame.Application.Services.GameweekTeams
{
    public class GameweekTeamsService : IGameweekTeamsService
    {
        private readonly IGameweekTeamsRepository _gameweekTeamsRepo;
        private readonly IGameweeksRepository _gameweeksRepo;
        private readonly IFanatsyTeamPlayersRepository _fantasyPlayersRepo;
        private readonly IGameweekTeamPlayersRepository _fanatsyTeamPlayersRepository;

        public GameweekTeamsService(
            IGameweekTeamsRepository gameweekTeamsRepo,
            IGameweeksRepository gameweeksRepo,
            IFanatsyTeamPlayersRepository fantasyPlayersRepo,
            IGameweekTeamPlayersRepository fanatsyTeamPlayersRepository)
        {
            _gameweekTeamsRepo = gameweekTeamsRepo;
            _gameweeksRepo = gameweeksRepo;
            _fantasyPlayersRepo = fantasyPlayersRepo;
            _fanatsyTeamPlayersRepository = fanatsyTeamPlayersRepository;
        }
        public async Task<GameweekTeam> Create(int fantasyTeamId)
        {
            var currentGameweek = await _gameweeksRepo.GetCurrentGameweek();
            if (currentGameweek == null)
                throw new Exception("No active gameweek found");

            var gameweekTeam = new GameweekTeam
            {
                FantasyTeamId = fantasyTeamId,
                GameweekId = currentGameweek.Id,
                HasUnlimitedTransfers = true, 
                TotalPoints = 0,
                TotalTransfers = 0
            };

            await _gameweekTeamsRepo.Create(gameweekTeam);
            await _gameweekTeamsRepo.Save();
            var fantasyTeamPlayers = await _fantasyPlayersRepo.GetByTeam(fantasyTeamId);
            foreach (var fantasyPlayer in fantasyTeamPlayers)
            {
                var gameweekPlayer = new GameweekTeamPlayer
                {
                    GameweekTeamId = gameweekTeam.Id,
                    FantasyTeamPlayerId = fantasyPlayer.Id,
                    IsStarting = !(fantasyPlayer.Slot ==PlayerSlot.Def5 || fantasyPlayer.Slot == PlayerSlot.Mid4 || fantasyPlayer.Slot == PlayerSlot.Mid5),
                    IsCaptain = fantasyPlayer.Slot==PlayerSlot.Gk1,
                    IsViceCaptain = fantasyPlayer.Slot == PlayerSlot.Def1
                };
                await _fanatsyTeamPlayersRepository.Create(gameweekPlayer);
            }

            await _fanatsyTeamPlayersRepository.Save();
            return gameweekTeam;
        }
    }
}

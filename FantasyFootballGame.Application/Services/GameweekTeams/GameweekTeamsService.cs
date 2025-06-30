using AutoMapper;
using FantasyFootballGame.Application.DTOs.GameweekTeams;
using FantasyFootballGame.Application.Interfaces.FantasyTeams;
using FantasyFootballGame.Application.Interfaces.Gameweeks;
using FantasyFootballGame.Application.Interfaces.GameweekTeams;
using FantasyFootballGame.Application.Validators.GameweekTeams;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeams;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeams;
using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.Domain.Models;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.GameweekTeams
{
    public class GameweekTeamsService : IGameweekTeamsService
    {
        private readonly IGameweekTeamsRepository _gameweekTeamsRepo;
        private readonly IGameweeksRepository _gameweeksRepo;
        private readonly IFanatsyTeamPlayersRepository _fantasyPlayersRepo;
        private readonly IGameweekTeamPlayersRepository _fanatsyTeamPlayersRepository;
        private readonly IGameweeksService _gameweeksService;
        private readonly IFantasyTeamsRepository _fantasyTeamsRepository;

        private readonly SwapPlayersValidator _swapValidator;


        public GameweekTeamsService(
            IGameweekTeamsRepository gameweekTeamsRepo,
            IGameweeksRepository gameweeksRepo,
            IFanatsyTeamPlayersRepository fantasyPlayersRepo,
            IGameweekTeamPlayersRepository fanatsyTeamPlayersRepository,
            IFantasyTeamsRepository fantasyTeamsRepository,
            IGameweeksService gameweeksService,
            SwapPlayersValidator swapValidator)
        {
            _gameweekTeamsRepo = gameweekTeamsRepo;
            _gameweeksRepo = gameweeksRepo;
            _fantasyPlayersRepo = fantasyPlayersRepo;
            _fanatsyTeamPlayersRepository = fanatsyTeamPlayersRepository;
            _fantasyTeamsRepository=fantasyTeamsRepository;
            _gameweeksService = gameweeksService;
            _swapValidator = swapValidator;
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
            var startingPlayers = new List<GameweekTeamPlayer>();
            var benchPlayers = new List<GameweekTeamPlayer>();

            int gkCount = 0, defCount = 0, midCount = 0, fwdCount = 0;
            foreach (var fantasyPlayer in fantasyTeamPlayers)
            {
                var player = fantasyPlayer.Player; // assume Player is included via Include or eager loaded
                var gwPlayer = new GameweekTeamPlayer
                {
                    GameweekTeamId = gameweekTeam.Id,
                    FantasyTeamPlayerId = fantasyPlayer.Id,
                    PlayerId = player.Id,
                };
                switch (player.Position)
                {
                    case PlayerPosition.Goalkeeper:
                        if (gkCount < 1)
                        {
                            gwPlayer.IsStarting = true;
                            gkCount++;
                            startingPlayers.Add(gwPlayer);
                        }
                        else
                        {
                            gwPlayer.IsStarting = false;
                            benchPlayers.Insert(0, gwPlayer); // GK goes to pos 11
                        }
                        break;

                    case PlayerPosition.Defender:
                        if (defCount < 4)
                        {
                            gwPlayer.IsStarting = true;
                            defCount++;
                            startingPlayers.Add(gwPlayer);
                        }
                        else
                        {
                            gwPlayer.IsStarting = false;
                            benchPlayers.Add(gwPlayer);
                        }
                        break;

                    case PlayerPosition.Midfielder:
                        if (midCount < 3)
                        {
                            gwPlayer.IsStarting = true;
                            midCount++;
                            startingPlayers.Add(gwPlayer);
                        }
                        else
                        {
                            gwPlayer.IsStarting = false;
                            benchPlayers.Add(gwPlayer);
                        }
                        break;

                    case PlayerPosition.Forward:
                        if (fwdCount < 3)
                        {
                            gwPlayer.IsStarting = true;
                            fwdCount++;
                            startingPlayers.Add(gwPlayer);
                        }
                        else
                        {
                            gwPlayer.IsStarting = false;
                            benchPlayers.Add(gwPlayer);
                        }
                        break;
                }
                // Set PosNum
                int pos = 0;
                foreach (var p in startingPlayers)
                {
                    p.PosNum = pos++;
                }
                for (int i = 0; i < benchPlayers.Count; i++)
                {
                    benchPlayers[i].PosNum = 11 + i;
                }

                // Assign Captain and Vice-Captain from starters
                if (startingPlayers.Count >= 2)
                {
                    startingPlayers[0].IsCaptain = true;
                    startingPlayers[1].IsViceCaptain = true;
                }

                var allPlayers = startingPlayers.Concat(benchPlayers);
                foreach (var gameweekPlayer in allPlayers)
                {
                    await _fanatsyTeamPlayersRepository.Create(gameweekPlayer);
                }
            }

            await _fanatsyTeamPlayersRepository.Save();
            return gameweekTeam;
        }

        public async Task Swap(Guid userId,SwapPlayersDto dto)
        {
            _swapValidator.SetUserContext(userId);
            await _swapValidator.ValidateAndThrowAsync(dto);
            var fantasyTeam = await _fantasyTeamsRepository.GetByUserId(userId);
            var swaps = dto.Swaps;
            var currentGameweek = await _gameweeksService.GetCurrentGameweek();
            var gameweekTeam = await _gameweekTeamsRepo.GetCurrentGameweekTeam(fantasyTeam.Id, currentGameweek.Id);
            foreach (var swap in swaps)
            {
                var playerOutId = swap.PlayerOutId;
                var playerInId = swap.PlayerInId;
                var gameweekPlayerOut = await _fanatsyTeamPlayersRepository.GetPlayerFromTeam(gameweekTeam.Id, playerOutId);
                var gameweekPlayerIn = await _fanatsyTeamPlayersRepository.GetPlayerFromTeam(gameweekTeam.Id, playerInId);
                gameweekPlayerOut.IsStarting = false;
                if (gameweekPlayerOut.IsCaptain)
                {
                    gameweekPlayerIn.IsCaptain = true;
                    gameweekPlayerOut.IsCaptain = false;
                }

                if (gameweekPlayerOut.IsViceCaptain)
                {
                    gameweekPlayerIn.IsViceCaptain = true;
                    gameweekPlayerOut.IsViceCaptain = false;
                }

                _fanatsyTeamPlayersRepository.Update(gameweekPlayerOut);
                _fanatsyTeamPlayersRepository.Update(gameweekPlayerIn);
            }

            await _fanatsyTeamPlayersRepository.Save();
        }
    }
}

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
        private readonly IFantasyTeamsService _fantasyTeamsService;
        private readonly IGameweeksService _gameweeksService;
        private readonly SwapPlayersValidator _swapValidator;


        public GameweekTeamsService(
            IGameweekTeamsRepository gameweekTeamsRepo,
            IGameweeksRepository gameweeksRepo,
            IFanatsyTeamPlayersRepository fantasyPlayersRepo,
            IGameweekTeamPlayersRepository fanatsyTeamPlayersRepository,
            IFantasyTeamsService fantasyTeamsService,
            IGameweeksService gameweeksService,
            SwapPlayersValidator swapValidator)
        {
            _gameweekTeamsRepo = gameweekTeamsRepo;
            _gameweeksRepo = gameweeksRepo;
            _fantasyPlayersRepo = fantasyPlayersRepo;
            _fanatsyTeamPlayersRepository = fanatsyTeamPlayersRepository;
            _fantasyTeamsService = fantasyTeamsService;
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

        public async Task Swap(int userId,SwapPlayersDto dto)
        {
            _swapValidator.SetUserContext(userId);
            await _swapValidator.ValidateAndThrowAsync(dto);
            var fantasyTeam = await _fantasyTeamsService.GetByUserId(userId);
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

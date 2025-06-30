using AutoMapper;
using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.Interfaces.Transfers;
using FantasyFootballGame.Application.Validators.FantasyTeams;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeams;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeams;
using FantasyFootballGame.DataAccess.Repositories.Transfers;
using FantasyFootballGame.Domain.Models.Actions;
using FluentValidation;

namespace FantasyFootballGame.Application.Services.Transfers
{
    public class TransfersService : ITransfersService
    {
        private readonly ITransfersRepository _transfersRepository;
        private readonly IFanatsyTeamPlayersRepository _fanatsyTeamPlayersRepository;
        private readonly IGameweekTeamsRepository _gameweekTeamsRepository;
        private readonly IGameweekTeamPlayersRepository _gameweekTeamPlayersRepository;
        private readonly IGameweeksRepository _gameweeksRepository;
        private readonly IFantasyTeamsRepository _fantasyTeamsRepository;
        private readonly IMapper _mapper;
        private readonly MakeTransfersValidator _makeTransfersValidator;

        public TransfersService(
            ITransfersRepository transfersRepository,
            IFanatsyTeamPlayersRepository fanatsyTeamPlayersRepository,
            IGameweekTeamsRepository gameweekTeamsRepository,
            IGameweekTeamPlayersRepository gameweekTeamPlayersRepository,
            IGameweeksRepository gameweeksRepository,
            IFantasyTeamsRepository fantasyTeamsRepository,
            IMapper mapper,
            MakeTransfersValidator makeTransfersValidator)
        {
            _fanatsyTeamPlayersRepository = fanatsyTeamPlayersRepository;
            _transfersRepository = transfersRepository;
            _mapper = mapper;
            _gameweekTeamsRepository = gameweekTeamsRepository;
            _gameweekTeamPlayersRepository = gameweekTeamPlayersRepository;
            _gameweeksRepository = gameweeksRepository;
            _makeTransfersValidator = makeTransfersValidator;
            _fantasyTeamsRepository = fantasyTeamsRepository;
        }

        public async Task Create(Guid userId,MakeTransfersDto dto)
        {

            var fantasyTeam = await _fantasyTeamsRepository.GetByUserId(userId);
            if (fantasyTeam == null)
                throw new Exception("No Fantasy team for user");

            await _makeTransfersValidator.ValidateAndThrowAsync(dto);
            var transfers = dto.Transfers;
            var currentGameweek = await _gameweeksRepository.GetCurrentGameweek();
            if (currentGameweek == null)
                throw new Exception("No active gameweek found");
            var gameweekTeam = await _gameweekTeamsRepository.GetCurrentGameweekTeam(fantasyTeam.Id, currentGameweek.Id);
            bool hasUnlimitedTransfers = gameweekTeam.HasUnlimitedTransfers;
            int freeTransfers = fantasyTeam.FreeTransfers;
            int transferCost = gameweekTeam.TransferCost;
            foreach (var tr in transfers)
            {
                var playerOutId = tr.PlayerOutId;
                var playerInId = tr.PlayerInId;
                var playerOut = await _fanatsyTeamPlayersRepository.GetPlayerFromTeam(fantasyTeam.Id, playerOutId);
                var gameweekPlayerOut = await _gameweekTeamPlayersRepository.GetPlayerFromTeam(gameweekTeam.Id, playerOutId);

                playerOut.PlayerId = playerInId;
                _fanatsyTeamPlayersRepository.Update(playerOut);

                gameweekPlayerOut.PlayerId = playerOutId;
                _gameweekTeamPlayersRepository.Update(gameweekPlayerOut);

                var transfer = _mapper.Map<Transfer>((fantasyTeam.Id, currentGameweek.Id, tr));
                await _transfersRepository.Create(transfer);

                if (!hasUnlimitedTransfers)
                {
                    freeTransfers -= (freeTransfers > 0) ? 1 : 0;
                    transferCost -= (freeTransfers == 0) ? 4 : 0;
                }
            }
            gameweekTeam.TransfersMade += transfers.Count;
            gameweekTeam.TransferCost += transferCost;
            fantasyTeam.FreeTransfers=freeTransfers;
            _gameweekTeamsRepository.Update(gameweekTeam);
            _fantasyTeamsRepository.Update(fantasyTeam);
            await _fantasyTeamsRepository.Save();
            await _gameweekTeamsRepository.Save();
            await _fanatsyTeamPlayersRepository.Save();
            await _gameweekTeamPlayersRepository.Save();
            await _transfersRepository.Save();
        }
    }
}

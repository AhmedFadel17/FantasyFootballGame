using AutoMapper;
using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.Interfaces.Transfers;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeamPlayers;
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
        private readonly IMapper _mapper;
        private readonly IValidator<MakeTransfersDto> _makeTransfersValidator;

        public TransfersService(
            ITransfersRepository transfersRepository,
            IFanatsyTeamPlayersRepository fanatsyTeamPlayersRepository,
            IGameweekTeamsRepository gameweekTeamsRepository,
            IGameweekTeamPlayersRepository gameweekTeamPlayersRepository,
            IGameweeksRepository gameweeksRepository,
            IMapper mapper,
            IValidator<MakeTransfersDto> makeTransfersValidator)
        {
            _fanatsyTeamPlayersRepository = fanatsyTeamPlayersRepository;
            _transfersRepository = transfersRepository;
            _mapper = mapper;
            _gameweekTeamsRepository = gameweekTeamsRepository;
            _gameweekTeamPlayersRepository = gameweekTeamPlayersRepository;
            _gameweeksRepository = gameweeksRepository;
            _makeTransfersValidator = makeTransfersValidator;
        }

        public async Task Create(MakeTransfersDto dto)
        {
            await _makeTransfersValidator.ValidateAndThrowAsync(dto);
            var transfers = dto.Transfers;
            var fantasyTeamId = dto.FantasyTeamId;
            var currentGameweek = await _gameweeksRepository.GetCurrentGameweek();
            if (currentGameweek == null)
                throw new Exception("No active gameweek found");
            var gameweekTeam = await _gameweekTeamsRepository.GetCurrentGameweekTeam(fantasyTeamId, currentGameweek.Id);
            bool hasUnlimitedTransfers = gameweekTeam.HasUnlimitedTransfers;
            int freeTransfers = gameweekTeam.FreeTransfers;
            int transferCost = gameweekTeam.TransferCost;
            foreach (var tr in transfers)
            {
                var playerOutId = tr.PlayerOutId;
                var playerInId = tr.PlayerInId;
                var playerOut = await _fanatsyTeamPlayersRepository.GetPlayerFromTeam(fantasyTeamId, playerOutId);
                var gameweekPlayerOut = await _gameweekTeamPlayersRepository.GetPlayerFromTeam(gameweekTeam.Id, playerOutId);

                playerOut.PlayerId = playerInId;
                _fanatsyTeamPlayersRepository.Update(playerOut);

                gameweekPlayerOut.PlayerId = playerOutId;
                _gameweekTeamPlayersRepository.Update(gameweekPlayerOut);

                var transfer = _mapper.Map<Transfer>((fantasyTeamId, currentGameweek.Id, tr));
                await _transfersRepository.Create(transfer);

                if (!hasUnlimitedTransfers)
                {
                    freeTransfers -= (freeTransfers > 0) ? 1 : 0;
                    transferCost -= (freeTransfers == 0) ? 4 : 0;
                }
            }
            gameweekTeam.TotalTransfers += transfers.Count;
            gameweekTeam.TransferCost += transferCost;
            _gameweekTeamsRepository.Update(gameweekTeam);
            await _gameweekTeamsRepository.Save();
            await _fanatsyTeamPlayersRepository.Save();
            await _gameweekTeamPlayersRepository.Save();
            await _transfersRepository.Save();
        }
    }
}

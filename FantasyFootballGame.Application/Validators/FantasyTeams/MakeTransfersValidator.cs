using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.Validators.Transfers;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeams;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.FantasyTeams
{
    public class MakeTransfersValidator : AbstractValidator<MakeTransfersDto>
    {
        private readonly IFantasyTeamsRepository _fantasyTeamsRepository;
        private readonly IPlayersRepository _playersRepository;

        public MakeTransfersValidator(
            IFantasyTeamsRepository fantasyTeamsRepository,
            IGameweeksRepository gameweeksRepository,
            IPlayersRepository playersRepository,
            CreateTransferValidator transferValidator)
        {
            _fantasyTeamsRepository = fantasyTeamsRepository;
            _playersRepository = playersRepository;

            RuleFor(t => t.FantasyTeamId)
                .MustAsync(async (id, cancellation) => await fantasyTeamsRepository.Exists(t => t.Id == id))
                .WithMessage("The specified FantasyTeamId does not exist.");

            RuleFor(t => t.GameweekId)
                .MustAsync(async (id, cancellation) => await gameweeksRepository.Exists(t => t.Id == id))
                .WithMessage("The specified GameweekId does not exist.");

            RuleFor(t => t.transfers)
                .NotNull().WithMessage("Transfers list cannot be null.")
                .NotEmpty().WithMessage("At least one transfer is required.")
                .ForEach(transfer => transfer.SetValidator(transferValidator));

            RuleFor(t => t)
                .MustAsync(async (dto, cancellation) => await ValidateTransfers(dto))
                .WithMessage("Invalid transfer: PlayerOut must be in the team, and PlayerIn must not be in the team.");

            RuleFor(t => t)
                .MustAsync(async (dto, cancellation) => await ValidateMaxPlayersPerTeam(dto))
                .WithMessage("Invalid transfer: A team cannot have more than 3 players from the same club.");

            RuleFor(t => t)
                .Must(dto => NoDuplicateTransfers(dto))
                .WithMessage("Invalid transfer: A player cannot be transferred in or out multiple times.");
        }

        private async Task<bool> ValidateTransfers(MakeTransfersDto dto)
        {
            var team = await _fantasyTeamsRepository.GetById(dto.FantasyTeamId);
            if (team == null) return false;

            var playerIdsInTeam = team.Players.Select(p => p.PlayerId).ToHashSet();

            foreach (var transfer in dto.transfers)
            {
                if (!playerIdsInTeam.Contains(transfer.PlayerOutId))
                {
                    return false; // PlayerOut is not in the team
                }

                if (playerIdsInTeam.Contains(transfer.PlayerInId))
                {
                    return false; // PlayerIn is already in the team
                }
            }
            return true;
        }

        private async Task<bool> ValidateMaxPlayersPerTeam(MakeTransfersDto dto)
        {
            var team = await _fantasyTeamsRepository.GetById(dto.FantasyTeamId);
            if (team == null) return false;

            var playerIdsInTeam = team.Players.Select(p => p.PlayerId).ToList();

            // Simulate transfers: Remove outgoing players, add incoming players
            foreach (var transfer in dto.transfers)
            {
                playerIdsInTeam.Remove(transfer.PlayerOutId);
                playerIdsInTeam.Add(transfer.PlayerInId);
            }

            // Get players' club info
            var players = await _playersRepository.GetByIds(playerIdsInTeam);
            var teamCounts = players.GroupBy(p => p.TeamId).ToDictionary(g => g.Key, g => g.Count());

            // Ensure no club has more than 3 players
            return teamCounts.Values.All(count => count <= 3);
        }

        private bool NoDuplicateTransfers(MakeTransfersDto dto)
        {
            var playerInIds = dto.transfers.Select(t => t.PlayerInId).ToList();
            var playerOutIds = dto.transfers.Select(t => t.PlayerOutId).ToList();

            return playerInIds.Distinct().Count() == playerInIds.Count &&
                   playerOutIds.Distinct().Count() == playerOutIds.Count;
        }
    }
}

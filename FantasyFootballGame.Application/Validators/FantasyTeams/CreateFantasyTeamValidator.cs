using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;
using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.Validators.FantasyTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeams;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.Domain.Enums;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.FantasyTeams
{
    public class CreateFantasyTeamValidator : AbstractValidator<CreateFantasyTeamDto>
    {
        private readonly IPlayersRepository _playersRepository;
        public CreateFantasyTeamValidator(
            IFantasyTeamsRepository fantasyTeamsRepository,
            IPlayersRepository playersRepository,
            CreateFantasyTeamPlayerValidator playerValidator)
        {
            _playersRepository = playersRepository;
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Team name is required.")
                .MinimumLength(3).WithMessage("Team name must be at least 3 characters.")
                .MaximumLength(255).WithMessage("Team name cannot exceed 255 characters.")
                .MustAsync(async (name, cancellation) => !await fantasyTeamsRepository.Exists(t => t.Name == name))
                .WithMessage("A team with this name already exists.");

            RuleFor(t => t.UserId)
                .GreaterThan(0).WithMessage("User ID must be a valid positive number.");

            RuleFor(t => t.Players)
                .NotNull().WithMessage("Players list cannot be null.")
                .NotEmpty().WithMessage("A team must have at least one player.")
                .Must(players => players.Count == 15).WithMessage("A team must have exactly 15 players.")
                .Must(players => players.GroupBy(p => p.PlayerId).All(g => g.Count() == 1))
                .WithMessage("Each player must be unique in the team.")
                .MustAsync(async (players, cancellation) =>
                {
                    var playerTeams = await Task.WhenAll(players.Select(async p => await GetTeamId(p.PlayerId)));
                    return playerTeams.GroupBy(teamId => teamId).All(g => g.Count() <= 3);
                })
                .WithMessage("A team cannot have more than 3 players from the same team.")
                .Must(players => players.Select(p => p.Slot).Distinct().Count() == players.Count)
                .WithMessage("Each player must have a unique slot.")
                .MustAsync(async (players, cancellation) =>
                {
                    var validationResults = await Task.WhenAll(players.Select(IsValidPositionForSlot));
                    return validationResults.All(result => result);
                })
                .WithMessage("Invalid player position for the assigned slot.");
            RuleFor(t => t.Players)
                .MustAsync(async (players, cancellation) =>
                {
                    var totalPrice = await CalculateTotalPrice(players);
                    return totalPrice <= 100;
                })
                .WithMessage("The total value of selected players cannot exceed 100.");
            RuleForEach(t => t.Players).SetValidator(playerValidator);
        }

        private async Task<int> GetTeamId(int playerId)
        {
            var p= await _playersRepository.GetById(playerId);
            return p.TeamId;
        }

        private async Task<bool> IsValidPositionForSlot(CreateFantasyTeamPlayerDto player)
        {
            var playerPosition = await GetPlayerPosition(player.PlayerId);
            return player.Slot switch
            {
                PlayerSlot.Gk1 or PlayerSlot.Gk2 => playerPosition == PlayerPosition.Goalkeeper,
                PlayerSlot.Def1 or PlayerSlot.Def2 or PlayerSlot.Def3 or PlayerSlot.Def4 or PlayerSlot.Def5 => playerPosition == PlayerPosition.Defender,
                PlayerSlot.Mid1 or PlayerSlot.Mid2 or PlayerSlot.Mid3 or PlayerSlot.Mid4 or PlayerSlot.Mid5 => playerPosition == PlayerPosition.Midfielder,
                PlayerSlot.Frw1 or PlayerSlot.Frw2 or PlayerSlot.Frw3 => playerPosition == PlayerPosition.Forward,
                _ => false
            };
        }

        private async Task<PlayerPosition> GetPlayerPosition(int playerId)
        {
            var p = await _playersRepository.GetById(playerId);
            return p.Position;
        }

        private async Task<double> CalculateTotalPrice(List<CreateFantasyTeamPlayerDto> players)
        {
            var playerIds = players.Select(p => p.PlayerId).ToList();
            var playersData = await _playersRepository.GetByIds(playerIds);
            return playersData.Sum(p => p.Price);
        }
    }
}

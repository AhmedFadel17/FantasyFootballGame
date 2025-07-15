using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;
using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.Application.Validators.FantasyTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeams;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.Domain.Enums.Players;
using FantasyFootballGame.Domain.Models;
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
                .MustAsync(async (name, cancellation) =>
                    !await fantasyTeamsRepository.Exists(t => t.Name == name))
                .WithMessage("A team with this name already exists.");

            RuleFor(t => t.Players)
                .NotNull().WithMessage("Players list cannot be null.")
                .NotEmpty().WithMessage("A team must have at least one player.")
                .Must(players => players.Count == 15).WithMessage("A team must have exactly 15 players.")
                .Must(players => players.GroupBy(p => p.PlayerId).All(g => g.Count() == 1))
                .WithMessage("Each player must be unique in the team.")
                .MustAsync(HasNoMoreThanThreeFromSameTeam)
                .WithMessage("A team cannot have more than 3 players from the same team.")
                .MustAsync(HasValidPositionCounts)
                .WithMessage("Team must include exactly 2 Goalkeepers, 5 Defenders, 5 Midfielders, and 3 Forwards.")
                .MustAsync(async (players, cancellation) =>
                {
                    var totalPrice = await CalculateTotalPrice(players);
                    return totalPrice <= 100;
                })
                .WithMessage("The total value of selected players cannot exceed 100.");

            RuleForEach(t => t.Players).SetValidator(playerValidator);
        }

        private async Task<bool> HasNoMoreThanThreeFromSameTeam(List<CreateFantasyTeamPlayerDto> players, CancellationToken token)
        {
            var teamCounts = new Dictionary<int, int>();

            foreach (var player in players)
            {
                var teamId = await GetTeamId(player.PlayerId);
                if (teamCounts.ContainsKey(teamId))
                    teamCounts[teamId]++;
                else
                    teamCounts[teamId] = 1;

                if (teamCounts[teamId] > 3)
                    return false;
            }

            return true;
        }

        private async Task<int> GetTeamId(int playerId)
        {
            var player = await _playersRepository.GetById(playerId);
            return player.TeamId;
        }

        private async Task<bool> HasValidPositionCounts(List<CreateFantasyTeamPlayerDto> players, CancellationToken token)
        {
            var positions = new List<PlayerPosition>();

            foreach (var p in players)
            {
                var pl = await _playersRepository.GetById(p.PlayerId);
                positions.Add(pl.Position);
            }

            var gk = positions.Count(p => p == PlayerPosition.Goalkeeper);
            var def = positions.Count(p => p == PlayerPosition.Defender);
            var mid = positions.Count(p => p == PlayerPosition.Midfielder);
            var fwd = positions.Count(p => p == PlayerPosition.Forward);

            return gk == 2 && def == 5 && mid == 5 && fwd == 3;
        }


        private async Task<double> CalculateTotalPrice(List<CreateFantasyTeamPlayerDto> players)
        {
            double total = 0;
            foreach (var player in players)
            {
                var data = await _playersRepository.GetById(player.PlayerId);
                total += data.Price;
            }
            return total;
        }
    }
}

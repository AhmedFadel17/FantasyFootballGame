using FantasyFootballGame.Application.DTOs.GameActions.Saves;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Saves
{
    public class CreateSaveValidator : AbstractValidator<CreateSaveDto>
    {
        public CreateSaveValidator(IPlayersRepository playersRepository, ITeamsRepository teamsRepository, IFixturesRepository fixturesRepository)
        {
            RuleFor(p => p.Minute)
                .InclusiveBetween(1, 120).WithMessage("Minute must be between 1 and 120.");

            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) => await playersRepository.Exists(p => p.Id == playerId))
                .WithMessage("The specified PlayerId does not exist.");

            RuleFor(p => p.TeamId)
                .MustAsync(async (teamId, cancellation) => await teamsRepository.Exists(t => t.Id == teamId))
                .WithMessage("The specified TeamId does not exist.");

            RuleFor(p => new { p.FixtureId, p.TeamId })
                .MustAsync(async (data, cancellation) =>
                    await fixturesRepository.Exists(f =>
                        f.Id == data.FixtureId &&
                        (f.HomeTeamId == data.TeamId || f.AwayTeamId == data.TeamId)))
                .WithMessage("The specified TeamId is not part of the Fixture.");
        }
    }
}

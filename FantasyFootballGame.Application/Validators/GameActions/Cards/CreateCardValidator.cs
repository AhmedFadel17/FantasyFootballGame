using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FluentValidation;
using FantasyFootballGame.Application.DTOs.GameActions.Cards;

namespace FantasyFootballGame.Application.Validators.GameActions.Cards
{
    public class CreateCardValidator : AbstractValidator<CreateCardDto>
    {
        public CreateCardValidator(
            IPlayersRepository playersRepository,
            ITeamsRepository teamsRepository,
            IFixturesRepository fixturesRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) =>
                    await playersRepository.Exists(pl => pl.Id == playerId))
                .WithMessage("The specified PlayerId does not exist.");

            RuleFor(p => p.TeamId)
                .MustAsync(async (teamId, cancellation) =>
                    await teamsRepository.Exists(t => t.Id == teamId))
                .WithMessage("The specified TeamId does not exist.");

            RuleFor(p => p.FixtureId)
                .MustAsync(async (fixtureId, cancellation) =>
                    await fixturesRepository.Exists(f => f.Id == fixtureId))
                .WithMessage("The specified FixtureId does not exist.");

            RuleFor(p => p.Minute)
                .InclusiveBetween(1, 120)
                .WithMessage("Minute must be between 1 and 120.");

            RuleFor(p => p.CardType)
                .IsInEnum()
                .WithMessage("Invalid CardType value.");
        }
    }
}

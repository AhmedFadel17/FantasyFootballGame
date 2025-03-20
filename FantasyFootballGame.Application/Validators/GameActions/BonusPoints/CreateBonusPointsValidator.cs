using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints;
using FantasyFootballGame.Application.Validators.GameActions.BonusPoints.IndividualBonus;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.BonusPoints
{
    public class CreateBonusPointsValidator : AbstractValidator<CreateBonusPointsDto>
    {
        public CreateBonusPointsValidator(
            IFixturesRepository fixturesRepository,
            IPlayersRepository playersRepository)
        {
            RuleFor(p => p.FixtureId)
                .MustAsync(async (fixtureId, cancellation) =>
                    await fixturesRepository.Exists(f => f.Id == fixtureId))
                .WithMessage("The specified FixtureId does not exist.");

            RuleFor(p => p.BonusPoints)
                .NotEmpty()
                .WithMessage("BonusPoints list cannot be empty.")
                .Must(points => points.Count <= 3)
                .WithMessage("A maximum of 3 players can receive bonus points.");

            RuleForEach(p => p.BonusPoints)
                .SetValidator(new CreateBonusValidator(playersRepository));
        }
    }

    
}

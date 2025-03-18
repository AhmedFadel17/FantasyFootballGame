using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints;
using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints.IndvidualBonusPoints;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.BonusPoints.IndividualBonus
{
    public class CreateBonusValidator : AbstractValidator<CreateBonusPointDto>
    {
        public CreateBonusValidator(IPlayersRepository playersRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) =>
                    await playersRepository.Exists(pl => pl.Id == playerId))
                .WithMessage("The specified PlayerId does not exist.");

            RuleFor(p => p.Points)
                .InclusiveBetween(1, 3)
                .WithMessage("Bonus points must be between 1 and 3.");
        }
    }
}

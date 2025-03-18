using FantasyFootballGame.Application.DTOs.GameActions.BonusPoints;
using FantasyFootballGame.Application.Validators.GameActions.BonusPoints.IndividualBonus;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.BonusPoints
{
    public class UpdateBonusPointsValidator : AbstractValidator<UpdateBonusPointsDto>
    {
        public UpdateBonusPointsValidator(IPlayersRepository playersRepository)
        {
            RuleFor(p => p.BonusPoints)
                .NotEmpty()
                .WithMessage("BonusPoints list cannot be empty.")
                .Must(points => points.Count <= 3)
                .WithMessage("A maximum of 3 players can receive bonus points.");

            RuleForEach(p => p.BonusPoints)
                .SetValidator(new CreateBonusPointValidator(playersRepository));
        }
    }

    
}

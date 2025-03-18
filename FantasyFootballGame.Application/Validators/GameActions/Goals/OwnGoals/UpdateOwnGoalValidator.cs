using FantasyFootballGame.Application.DTOs.GameActions.Goals.OwnGoals;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Goals.OwnGoals
{
    public class UpdateOwnGoalValidator : AbstractValidator<UpdateOwnGoalDto>
    {
        public UpdateOwnGoalValidator(IPlayersRepository playersRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) =>
                    playerId == null || await playersRepository.Exists(pl => pl.Id == playerId))
                .WithMessage("The specified PlayerId does not exist.")
                .When(p => p.PlayerId.HasValue);
        }
    }
}

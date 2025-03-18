using FantasyFootballGame.Application.DTOs.GameActions.Goals.GoalsScored;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Goals.GoalScored
{
    public class UpdateGoalScoredValidator : AbstractValidator<UpdateGoalScoredDto>
    {
        public UpdateGoalScoredValidator(IPlayersRepository playersRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) =>
                    playerId == null || await playersRepository.Exists(pl => pl.Id == playerId))
                .WithMessage("The specified PlayerId does not exist.")
                .When(p => p.PlayerId.HasValue);
        }
    }
}

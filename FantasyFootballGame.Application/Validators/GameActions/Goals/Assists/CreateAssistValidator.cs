using FantasyFootballGame.Application.DTOs.GameActions.Goals.Assists;
using FantasyFootballGame.DataAccess.Repositories.Actions.Goals;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Goals.Assists
{
    public class CreateAssistValidator : AbstractValidator<CreateAssistDto>
    {
        public CreateAssistValidator(IGoalsRepository goalsRepository, IPlayersRepository playersRepository)
        {
            RuleFor(p => p.GoalId)
                .MustAsync(async (goalId, cancellation) =>
                    await goalsRepository.Exists(g => g.Id == goalId))
                .WithMessage("The specified GoalId does not exist.");

            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) =>
                    playerId == null || await playersRepository.Exists(pl => pl.Id == playerId))
                .WithMessage("The specified PlayerId does not exist.")
                .When(p => p.PlayerId.HasValue);
        }
    }
}

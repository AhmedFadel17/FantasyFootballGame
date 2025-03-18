using FluentValidation;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Actions.Goals;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FantasyFootballGame.DataAccess.Repositories.Actions.Penalties;

public class UpdatePenaltyValidator : AbstractValidator<UpdatePenaltyDto>
{
    public UpdatePenaltyValidator(
        IGoalsRepository goalsRepository,
        IPenaltiesRepository penaltiesRepository,
        IFixturesRepository fixturesRepository)
    {
        RuleFor(x => x.Minute)
            .InclusiveBetween(0, 120).When(x => x.Minute.HasValue)
            .WithMessage("Minute must be between 0 and 120.");

        RuleFor(x => x.GoalId)
            .MustAsync(async (goalId, cancellation) =>
                await goalsRepository.Exists(g => g.Id == goalId))
            .When(x => x.GoalId.HasValue)
            .WithMessage("The specified GoalId does not exist.");

        RuleFor(x => x.GoalId)
            .MustAsync(async (dto, goalId, cancellation) =>
            {
                if (!goalId.HasValue) return true;
                var goal = await goalsRepository.GetById(goalId.Value);
                var penalty = await penaltiesRepository.GetById(dto.GoalId.Value);
                return goal != null && penalty != null && goal.FixtureId == penalty.FixtureId;
            })
            .When(x => x.GoalId.HasValue)
            .WithMessage("GoalId must belong to the same fixture as the penalty.");

        // ✅ Ensure GoalId is not already used in another penalty
        RuleFor(x => x.GoalId)
            .MustAsync(async (goalId, cancellation) =>
            {
                if (!goalId.HasValue) return true;
                return !await penaltiesRepository.Exists(p => p.GoalId == goalId.Value);
            })
            .When(x => x.GoalId.HasValue)
            .WithMessage("This GoalId is already linked to another penalty.");

        // ✅ Ensure GoalId is provided when IsScored is true
        RuleFor(x => x.GoalId)
            .NotNull()
            .When(x => x.IsScored.HasValue && x.IsScored.Value)
            .WithMessage("GoalId must be provided when IsScored is true.");
    }
}

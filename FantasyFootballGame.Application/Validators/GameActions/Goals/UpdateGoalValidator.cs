using FantasyFootballGame.Application.DTOs.GameActions.Goals;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Goals
{
    public class UpdateGoalValidator : AbstractValidator<UpdateGoalDto>
    {
        public UpdateGoalValidator()
        {
            RuleFor(p => p.Minute)
                .InclusiveBetween(1, 120).WithMessage("Minute must be between 1 and 120.")
                .When(p => p.Minute.HasValue);
        }
    }
}

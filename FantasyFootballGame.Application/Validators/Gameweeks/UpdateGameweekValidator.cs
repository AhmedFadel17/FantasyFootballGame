using FantasyFootballGame.Application.DTOs.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.Gameweeks
{
    public class UpdateGameweekValidator : AbstractValidator<UpdateGameweekDto>
    {
        public UpdateGameweekValidator(IGameweeksRepository gameweeksRepository)
        {
            RuleFor(g => g.Name)
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.")
                .When(g => g.Name is not null);

            RuleFor(g => g.Description)
                .MaximumLength(555).WithMessage("Description cannot exceed 555 characters.")
                .When(g => g.Description is not null);

            RuleFor(g => g.WeekNumber)
                .InclusiveBetween(1, 99).WithMessage("Week Number must be between 1 and 99.")
                .MustAsync(async (weekNumber, cancellation) => !await gameweeksRepository.Exists(t => t.WeekNumber == weekNumber))
                .When(g => g.WeekNumber.HasValue);

            RuleFor(g => g.HighestPoints)
                .GreaterThanOrEqualTo(0).WithMessage("Highest Points must be 0 or more.")
                .When(g => g.HighestPoints.HasValue);

            RuleFor(g => g.Deadline)
                .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be in the future.")
                .When(g => g.Deadline.HasValue);
        }
    }
}

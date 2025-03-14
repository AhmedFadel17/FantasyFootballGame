using FantasyFootballGame.Application.DTOs.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.Gameweeks
{
    public class CreateGameweekValidator : AbstractValidator<CreateGameweekDto>
    {
        public CreateGameweekValidator(IGameweeksRepository gameweeksRepository)
        {
            RuleFor(g => g.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(g => g.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(555).WithMessage("Description cannot exceed 555 characters.");

            RuleFor(g => g.WeekNumber)
                .InclusiveBetween(1, 99).WithMessage("Week number must be between 1 and 99.")
                .MustAsync(async (weekNumber, cancellation) => !await gameweeksRepository.Exists(t => t.WeekNumber == weekNumber))
                .WithMessage("Week number must be unique.");

            RuleFor(g => g.HighestPoints)
                .GreaterThanOrEqualTo(0).WithMessage("Highest points must be at least 0.");

            RuleFor(g => g.Deadline)
                .NotEmpty().WithMessage("Deadline is required.")
                .GreaterThan(DateTime.UtcNow).WithMessage("Deadline must be in the future.");
        }
    }
}

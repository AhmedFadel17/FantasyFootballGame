using FantasyFootballGame.Application.DTOs.FantasyTeams;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeams;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.FantasyTeams
{
    public class UpdateFantasyTeamValidator : AbstractValidator<UpdateFantasyTeamDto>
    {
        public UpdateFantasyTeamValidator(IFantasyTeamsRepository fantasyTeamsRepository)
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Team name is required.")
                .MinimumLength(3).WithMessage("Team name must be at least 3 characters.")
                .MaximumLength(255).WithMessage("Team name cannot exceed 255 characters.")
                .MustAsync(async (name, cancellation) => !await fantasyTeamsRepository.Exists(t => t.Name == name))
                .WithMessage("A team with this name already exists.")
                .When(g => g.Name is not null);

            RuleFor(t => t.TotalPoints)
                .GreaterThanOrEqualTo(0).WithMessage("Total points cannot be negative.")
                .When(p => p.TotalPoints.HasValue);

            RuleFor(t => t.SquadValue)
                .GreaterThanOrEqualTo(0).WithMessage("Squad value cannot be negative.")
                .When(p => p.SquadValue.HasValue);

            RuleFor(t => t.InTheBank)
                .GreaterThanOrEqualTo(0).WithMessage("In the bank amount cannot be negative.")
                .When(p => p.InTheBank.HasValue);

        }
    }
}

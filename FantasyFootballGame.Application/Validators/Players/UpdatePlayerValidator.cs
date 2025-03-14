using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.Players
{
    public class UpdatePlayerValidator : AbstractValidator<UpdatePlayerDto>
    {
        public UpdatePlayerValidator(ITeamsRepository teamsRepository)
        {
            RuleFor(p => p.Name)
            .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
            .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.")
            .When(p => p.Name is not null); // Only validate if it's provided

            RuleFor(p => p.FullName)
                .MinimumLength(3).WithMessage("Full Name must be at least 3 characters long.")
                .MaximumLength(255).WithMessage("Full Name cannot exceed 255 characters.")
                .When(p => p.FullName is not null);

            RuleFor(p => p.Price)
                .InclusiveBetween(1, 99).WithMessage("Price must be between 1 and 99.")
                .When(p => p.Price.HasValue);

            RuleFor(p => p.ShirtNumber)
                .InclusiveBetween(1, 99).WithMessage("Shirt Number must be between 1 and 99.")
                .When(p => p.ShirtNumber.HasValue);

            RuleFor(p => p.Position)
                .IsInEnum().WithMessage("Invalid player position.")
                .When(p => p.Position.HasValue);

            RuleFor(p => p.Status)
                .IsInEnum().WithMessage("Invalid player status.")
                .When(p => p.Status.HasValue);

            RuleFor(p => p.TeamId)
                .MustAsync(async (teamId, cancellation) => await teamsRepository.Exists(t => t.Id == teamId))
                .WithMessage("The specified TeamId does not exist.")
                .When(p => p.TeamId.HasValue);
        }
    }
}

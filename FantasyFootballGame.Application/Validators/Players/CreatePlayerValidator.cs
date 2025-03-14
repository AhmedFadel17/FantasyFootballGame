using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.Players
{
    public class CreatePlayerValidator : AbstractValidator<CreatePlayerDto>
    {
        public CreatePlayerValidator(ITeamsRepository teamRepository)
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(p => p.FullName)
                .NotEmpty().WithMessage("Full Name is required.")
                .MinimumLength(3).WithMessage("Full Name must be at least 3 characters.")
                .MaximumLength(255).WithMessage("Full Name cannot exceed 255 characters.");

            RuleFor(p => p.Price)
                .InclusiveBetween(1, 99).WithMessage("Price must be between 1 and 99.");

            RuleFor(p => p.ShirtNumber)
                .InclusiveBetween(1, 99).WithMessage("Shirt Number must be between 1 and 99.");

            RuleFor(p => p.Position)
                .IsInEnum().WithMessage("Invalid player position.");

            RuleFor(p => p.TeamId)
                .MustAsync(async (teamId, cancellation) => await teamRepository.Exists(t => t.Id == teamId))
                .WithMessage("The specified TeamId does not exist.");
        }
    }

}
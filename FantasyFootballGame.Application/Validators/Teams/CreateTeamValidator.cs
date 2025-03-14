using FluentValidation;
using FantasyFootballGame.Application.DTOs.Teams;

namespace FantasyFootballGame.Application.Validators.Teams
{
    public class CreateTeamValidator : AbstractValidator<CreateTeamDto>
    {
        public CreateTeamValidator()
        {
            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long.")
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(t => t.Abbreviation)
                .NotEmpty().WithMessage("Abbreviation is required.")
                .MinimumLength(3).WithMessage("Abbreviation must be at least 3 characters long.")
                .MaximumLength(4).WithMessage("Abbreviation cannot exceed 4 characters.");

            RuleFor(t => t.MainColor)
                .NotEmpty().WithMessage("Main Color is required.")
                .MinimumLength(3).WithMessage("Main Color must be at least 3 characters long.")
                .MaximumLength(10).WithMessage("Main Color cannot exceed 10 characters.");

            RuleFor(t => t.SecondaryColor)
                .NotEmpty().WithMessage("Secondary Color is required.")
                .MinimumLength(3).WithMessage("Secondary Color must be at least 3 characters long.")
                .MaximumLength(10).WithMessage("Secondary Color cannot exceed 10 characters.");

            RuleFor(t => t.ShirtImgSrc)
                .NotEmpty().WithMessage("Shirt image source is required.")
                .MaximumLength(455).WithMessage("Shirt image source cannot exceed 455 characters.");
        }
    }

}
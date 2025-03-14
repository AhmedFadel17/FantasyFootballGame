using FantasyFootballGame.Application.DTOs.Teams;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.Teams
{
    public class UpdateTeamValidator : AbstractValidator<UpdateTeamDto>
    {
        public UpdateTeamValidator()
        {
            RuleFor(t => t.Name)
                .MaximumLength(255).WithMessage("Name cannot exceed 255 characters.");

            RuleFor(t => t.Abbreviation)
                .MaximumLength(4).WithMessage("Abbreviation cannot exceed 4 characters.");

            RuleFor(t => t.MainColor)
                .MaximumLength(10).WithMessage("Main Color cannot exceed 10 characters.");

            RuleFor(t => t.SecondaryColor)
                .MaximumLength(10).WithMessage("Secondary Color cannot exceed 10 characters.");

            RuleFor(t => t.ShirtImgSrc)
                .MaximumLength(455).WithMessage("Shirt image source cannot exceed 455 characters.");
        }
    }
}






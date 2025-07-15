using FantasyFootballGame.Application.DTOs.Players;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FantasyFootballGame.Domain.Enums.Players;
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
                .Must(position => Enum.TryParse<PlayerPosition>(position, false, out _))
                .WithMessage("Invalid player position. Must be one of: " + string.Join(", ", Enum.GetNames(typeof(PlayerPosition))))
                .When(p => p.Position is not null);

            RuleFor(p => p.Status)
                .Must(status => Enum.TryParse<PlayerStatus>(status, false, out _))
                .WithMessage("Invalid player status. Must be one of: " + string.Join(", ", Enum.GetNames(typeof(PlayerStatus))))
                .When(p => p.Status is not null);

            RuleFor(p => p.TeamId)
                .MustAsync(async (teamId, cancellation) => await teamsRepository.Exists(t => t.Id == teamId))
                .WithMessage("The specified TeamId does not exist.")
                .When(p => p.TeamId.HasValue);

            RuleFor(p => p.Height)
               .InclusiveBetween(1, 300).WithMessage("Height must be between 1 and 300.")
               .When(p => p.Height.HasValue);
            RuleFor(p => p.Weight)
                .InclusiveBetween(1, 300).WithMessage("Weight must be between 1 and 300.")
               .When(p => p.Weight.HasValue);

            RuleFor(p => p.ImageSrc)
                .NotEmpty().WithMessage("Image Src is required.")
                .MaximumLength(455).WithMessage("image source cannot exceed 455 characters.")
               .When(p => p.ImageSrc is not null);


            RuleFor(p => p.Country)
                .NotEmpty().WithMessage("Country is required.")
                .MaximumLength(455).WithMessage("Country cannot exceed 99 characters.")
               .When(p => p.Country is not null);


            RuleFor(p => p.DateOfBirth)
                .NotEmpty().WithMessage("Date Of Birth is required.")
               .When(p => p.DateOfBirth.HasValue);

        }


    }
}

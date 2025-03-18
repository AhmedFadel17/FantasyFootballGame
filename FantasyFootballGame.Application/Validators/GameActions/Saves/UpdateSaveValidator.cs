using FantasyFootballGame.Application.DTOs.GameActions.Saves;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Saves
{
    public class UpdateSaveValidator : AbstractValidator<UpdateSaveDto>
    {
        public UpdateSaveValidator()
        {
            RuleFor(p => p.Minute)
                .InclusiveBetween(1, 120)
                .WithMessage("Minute must be between 1 and 120.");
        }
    }
}

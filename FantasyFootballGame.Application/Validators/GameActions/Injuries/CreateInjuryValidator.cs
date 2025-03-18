using FantasyFootballGame.Application.DTOs.GameActions.Injuries;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Injuries
{
    public class CreateInjuryValidator : AbstractValidator<CreateInjuryDto>
    {
        public CreateInjuryValidator(IPlayersRepository playersRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) =>
                    await playersRepository.Exists(pl => pl.Id == playerId))
                .WithMessage("The specified PlayerId does not exist.");

            RuleFor(p => p.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(p => p.Level)
                .IsInEnum().WithMessage("Invalid injury level.");

            RuleFor(p => p.EndDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("EndDate must be in the future.");
        }
    }
}

using FantasyFootballGame.Application.DTOs.GameActions.Injuries;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Injuries
{
    public class UpdateInjuryValidator : AbstractValidator<UpdateInjuryDto>
    {
        public UpdateInjuryValidator(IPlayersRepository playersRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) =>
                    playerId == null || await playersRepository.Exists(pl => pl.Id == playerId.Value))
                .WithMessage("The specified PlayerId does not exist.");

            RuleFor(p => p.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.")
                .When(p => !string.IsNullOrEmpty(p.Description));

            RuleFor(p => p.Level)
                .IsInEnum().WithMessage("Invalid injury level.")
                .When(p => p.Level.HasValue);

            RuleFor(p => p.EndDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("EndDate must be in the future.")
                .When(p => p.EndDate.HasValue);

            RuleFor(p => p.IsActive)
                .NotNull().WithMessage("IsActive must be specified.");
        }
    }
}

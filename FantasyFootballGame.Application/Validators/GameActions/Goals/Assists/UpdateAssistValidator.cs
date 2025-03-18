using FantasyFootballGame.Application.DTOs.GameActions.Goals.Assists;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Goals.Assists
{
    public class UpdateAssistValidator : AbstractValidator<UpdateAssistDto>
    {
        public UpdateAssistValidator(IPlayersRepository playersRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) =>
                    playerId == null || await playersRepository.Exists(pl => pl.Id == playerId))
                .WithMessage("The specified PlayerId does not exist.")
                .When(p => p.PlayerId.HasValue);
        }
    }
}

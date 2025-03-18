using FantasyFootballGame.Application.DTOs.GameActions.Cards;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Cards
{
    public class UpdateCardValidator : AbstractValidator<UpdateCardDto>
    {
        public UpdateCardValidator(
            IPlayersRepository playersRepository,
            ITeamsRepository teamsRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) =>
                    !playerId.HasValue || await playersRepository.Exists(pl => pl.Id == playerId.Value))
                .WithMessage("The specified PlayerId does not exist.");

            RuleFor(p => p.TeamId)
                .MustAsync(async (teamId, cancellation) =>
                    !teamId.HasValue || await teamsRepository.Exists(t => t.Id == teamId.Value))
                .WithMessage("The specified TeamId does not exist.");

            RuleFor(p => p.Minute)
                .InclusiveBetween(1, 120)
                .When(p => p.Minute.HasValue)
                .WithMessage("Minute must be between 1 and 120.");

            RuleFor(p => p.CardType)
                .IsInEnum()
                .When(p => p.CardType.HasValue)
                .WithMessage("Invalid CardType value.");
        }
    }
}

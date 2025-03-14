using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;
using FantasyFootballGame.DataAccess;
using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.FantasyTeamPlayers
{
    public class CreateFantasyTeamPlayerValidator : AbstractValidator<CreateFantasyTeamPlayerDto>
    {

        public CreateFantasyTeamPlayerValidator(IPlayersRepository playersRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (id, cancellation) => await playersRepository.Exists(t => t.Id == id))
                .WithMessage("Player does not exist.");

            RuleFor(p => p.Slot)
                .IsInEnum().WithMessage("Invalid player slot selected.");
        }
    }
}

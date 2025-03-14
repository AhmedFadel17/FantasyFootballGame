using FantasyFootballGame.Application.DTOs.FantasyTeamPlayers;
using FantasyFootballGame.DataAccess;
using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace FantasyFootballGame.Application.Validators.FantasyTeamPlayers
{
    public class UpdateFantasyTeamPlayerValidator : AbstractValidator<UpdateFantasyTeamPlayerDto>
    {
        public UpdateFantasyTeamPlayerValidator(IPlayersRepository playersRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (id, cancellation) => await playersRepository.Exists(t => t.Id == id))
                .WithMessage("Player does not exist.")
                .When(p => p.PlayerId.HasValue);

            RuleFor(p => p.Slot)
                .IsInEnum().WithMessage("Invalid player slot selected.")
                .When(p => p.Slot.HasValue);
        }
    }
}

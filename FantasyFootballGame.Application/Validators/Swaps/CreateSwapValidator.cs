using FantasyFootballGame.Application.DTOs.Swaps;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeamPlayers;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.Swaps
{
    public class CreateSwapValidator :AbstractValidator<CreateSwapDto>
    {
        public CreateSwapValidator(IGameweekTeamPlayersRepository gameweekTeamPlayersRepository)
        {
            RuleFor(t => t.PlayerInId)
                .MustAsync(async (id, cancellation) => await gameweekTeamPlayersRepository.Exists(t => t.Id == id && t.IsStarting==false))
                .WithMessage("The specified PlayerInId does not exist on your bench.");
            RuleFor(t => t.PlayerOutId)
                .MustAsync(async (id, cancellation) => await gameweekTeamPlayersRepository.Exists(t => t.Id == id && t.IsStarting))
                .WithMessage("The specified PlayerOutId does not exist on your starting 11.");

            RuleFor(t => t)
                .Must(t => t.PlayerInId != t.PlayerOutId)
                .WithMessage("PlayerInId and PlayerOutId cannot be the same.");
        }
    }
}

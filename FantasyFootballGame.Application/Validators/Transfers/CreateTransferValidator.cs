using FantasyFootballGame.Application.DTOs.Transfers;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.Transfers
{
    public class CreateTransferValidator : AbstractValidator<CreateTransferDto>
    {
        private readonly IPlayersRepository _playersRepository;

        public CreateTransferValidator(IPlayersRepository playersRepository)
        {
            _playersRepository = playersRepository;

            RuleFor(t => t.PlayerInId)
                .MustAsync(async (id, cancellation) => await playersRepository.Exists(t => t.Id == id))
                .WithMessage("The specified PlayerInId does not exist.");

            RuleFor(t => t.PlayerOutId)
                .MustAsync(async (id, cancellation) => await playersRepository.Exists(t => t.Id == id))
                .WithMessage("The specified PlayerOutId does not exist.");

            RuleFor(t => t)
                .Must(t => t.PlayerInId != t.PlayerOutId)
                .WithMessage("PlayerInId and PlayerOutId cannot be the same.");

            RuleFor(t => t)
                .MustAsync(async (t, cancellation) => await HaveSamePosition(t.PlayerInId, t.PlayerOutId))
                .WithMessage("The transferred players must have the same position.");
        }

        private async Task<bool> HaveSamePosition(int playerInId, int playerOutId)
        {
            var playerIn = await _playersRepository.GetById(playerInId);
            var playerOut = await _playersRepository.GetById(playerOutId);

            if (playerIn == null || playerOut == null)
                return false;

            return playerIn.Position == playerOut.Position;
        }
    }
}

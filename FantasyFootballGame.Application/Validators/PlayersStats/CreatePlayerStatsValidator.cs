using FantasyFootballGame.Application.DTOs.PlayersStats;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.PlayersStats
{
    public class CreatePlayerStatsValidator : AbstractValidator<CreatePlayerStatsDto>
    {
        public CreatePlayerStatsValidator(
            IPlayersRepository playersRepository,
            IGameweeksRepository gameweeksRepository)
        {
            RuleFor(p => p.PlayerId)
                .MustAsync(async (playerId, cancellation) =>
                    await playersRepository.Exists(p => p.Id == playerId))
                .WithMessage("The specified PlayerId does not exist.");

            RuleFor(p => p.GameweekId)
                .MustAsync(async (gameweekId, cancellation) =>
                    await gameweeksRepository.Exists(g => g.Id == gameweekId))
                .WithMessage("The specified GameweekId does not exist.");

            RuleFor(p => p.TotalPoints)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Total points cannot be negative.");

            RuleFor(p => p.GoalsScored)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Goals scored cannot be negative.");

            RuleFor(p => p.Assists)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Assists cannot be negative.");

            RuleFor(p => p.CleanSheets)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Clean sheets cannot be negative.");

            RuleFor(p => p.OwnGoals)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Own goals cannot be negative.");

            RuleFor(p => p.PenaltiesSaved)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Penalties saved cannot be negative.");

            RuleFor(p => p.PenaltiesMissed)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Penalties missed cannot be negative.");

            RuleFor(p => p.YellowCards)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Yellow cards cannot be negative.");

            RuleFor(p => p.RedCards)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Red cards cannot be negative.");

            RuleFor(p => p.Saves)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Saves cannot be negative.");

            RuleFor(p => p.BonusPoints)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Bonus points cannot be negative.");
        }
    }
} 
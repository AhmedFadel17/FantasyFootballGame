using FantasyFootballGame.Application.DTOs.PlayersStats;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.PlayersStats
{
    public class UpdatePlayerStatsValidator : AbstractValidator<UpdatePlayerStatsDto>
    {
        public UpdatePlayerStatsValidator()
        {
            When(p => p.TotalPoints.HasValue, () =>
            {
                RuleFor(p => p.TotalPoints)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Total points cannot be negative.");
            });

            When(p => p.GoalsScored.HasValue, () =>
            {
                RuleFor(p => p.GoalsScored)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Goals scored cannot be negative.");
            });

            When(p => p.Assists.HasValue, () =>
            {
                RuleFor(p => p.Assists)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Assists cannot be negative.");
            });

            When(p => p.CleanSheets.HasValue, () =>
            {
                RuleFor(p => p.CleanSheets)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Clean sheets cannot be negative.");
            });

            When(p => p.OwnGoals.HasValue, () =>
            {
                RuleFor(p => p.OwnGoals)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Own goals cannot be negative.");
            });

            When(p => p.PenaltiesSaved.HasValue, () =>
            {
                RuleFor(p => p.PenaltiesSaved)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Penalties saved cannot be negative.");
            });

            When(p => p.PenaltiesMissed.HasValue, () =>
            {
                RuleFor(p => p.PenaltiesMissed)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Penalties missed cannot be negative.");
            });

            When(p => p.YellowCards.HasValue, () =>
            {
                RuleFor(p => p.YellowCards)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Yellow cards cannot be negative.");
            });

            When(p => p.RedCards.HasValue, () =>
            {
                RuleFor(p => p.RedCards)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Red cards cannot be negative.");
            });

            When(p => p.Saves.HasValue, () =>
            {
                RuleFor(p => p.Saves)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Saves cannot be negative.");
            });

            When(p => p.BonusPoints.HasValue, () =>
            {
                RuleFor(p => p.BonusPoints)
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("Bonus points cannot be negative.");
            });
        }
    }
} 
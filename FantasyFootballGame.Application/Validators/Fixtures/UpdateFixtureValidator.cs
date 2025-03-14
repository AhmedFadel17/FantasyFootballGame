using FantasyFootballGame.Application.DTOs.Fixtures;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.Fixtures
{
    public class UpdateFixtureValidator : AbstractValidator<UpdateFixtureDto>
    {
        public UpdateFixtureValidator(ITeamsRepository teamRepository, IGameweeksRepository gameweekRepository)
        {
            RuleFor(f => f.HomeTeamId)
                .GreaterThan(0).WithMessage("Home team must be a valid team.")
                .MustAsync(async (id, cancellation) => id == null || await teamRepository.Exists(t => t.Id == id))
                .WithMessage("Home team does not exist.")
                .When(f => f.HomeTeamId.HasValue);

            RuleFor(f => f.AwayTeamId)
                .GreaterThan(0).WithMessage("Away team must be a valid team.")
                .NotEqual(f => f.HomeTeamId).WithMessage("Home and Away teams cannot be the same.")
                .MustAsync(async (id, cancellation) => id == null || await teamRepository.Exists(t => t.Id == id))
                .WithMessage("Away team does not exist.")
                .When(f => f.AwayTeamId.HasValue);

            RuleFor(f => f.GameweekId)
                .GreaterThan(0).WithMessage("Gameweek must be valid.")
                .MustAsync(async (id, cancellation) => id == null || await gameweekRepository.Exists(t => t.Id == id))
                .WithMessage("Gameweek does not exist.")
                .When(f => f.GameweekId.HasValue);

            RuleFor(f => f.HomeTeamScore)
                .GreaterThanOrEqualTo(0).WithMessage("Home team score cannot be negative.")
                .When(f => f.HomeTeamScore.HasValue);

            RuleFor(f => f.AwayTeamScore)
                .GreaterThanOrEqualTo(0).WithMessage("Away team score cannot be negative.")
                .When(f => f.AwayTeamScore.HasValue);

            RuleFor(f => f.MatchTime)
                .GreaterThan(DateTime.UtcNow).WithMessage("Match time must be in the future.")
                .When(f => f.MatchTime.HasValue);
        }
    }
}

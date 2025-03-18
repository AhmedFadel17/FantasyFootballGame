using FantasyFootballGame.Application.DTOs.GameActions.Goals;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FluentValidation;

namespace FantasyFootballGame.Application.Validators.GameActions.Goals
{
    public class CreateGoalValidator : AbstractValidator<CreateGoalDto>
    {
        public CreateGoalValidator(ITeamsRepository teamsRepository, IPlayersRepository playersRepository, IFixturesRepository fixturesRepository)
        {
            RuleFor(p => p.Minute)
                .InclusiveBetween(1, 120).WithMessage("Minute must be between 1 and 120.");

            RuleFor(p => p.TeamId)
                .MustAsync(async (teamId, cancellation) => await teamsRepository.Exists(t => t.Id == teamId))
                .WithMessage("The specified TeamId does not exist.");

            RuleFor(p => new { p.FixtureId, p.TeamId })
                .MustAsync(async (data, cancellation) =>
                    await fixturesRepository.Exists(f =>
                        f.Id == data.FixtureId &&
                        (f.HomeTeamId == data.TeamId || f.AwayTeamId == data.TeamId)))
                .WithMessage("The specified TeamId is not part of the Fixture.");

            RuleFor(p => new { p.GoalScorerId, p.TeamId })
                .MustAsync(async (data, cancellation) =>
                    await playersRepository.Exists(f =>
                        f.Id == data.GoalScorerId &&
                        f.TeamId == data.TeamId))
                .WithMessage("The specified GoalScorerId is not part of the Team.")
                .When(p => p.GoalScorerId.HasValue);


            RuleFor(p => new { p.AssisterId, p.TeamId })
                .MustAsync(async (data, cancellation) =>
                    await playersRepository.Exists(f =>
                        f.Id == data.AssisterId &&
                        f.TeamId == data.TeamId))
                .WithMessage("The specified AssisterId is not part of the Team.")
                .When(p => p.AssisterId.HasValue);

            RuleFor(p => new { p.OwnGoalScorerId, p.TeamId, p.FixtureId })
                .MustAsync(async (data, cancellation) =>
                {
                    var fixture = await fixturesRepository.GetById(data.FixtureId);
                    if (fixture == null) return false;

                    var ownGoalScorer = await playersRepository.GetById(data.OwnGoalScorerId.Value);
                    if (ownGoalScorer == null) return false;

                    var opponentTeamId = fixture.HomeTeamId == data.TeamId ? fixture.AwayTeamId : fixture.HomeTeamId;
                    return ownGoalScorer.TeamId == opponentTeamId;
                })
                .WithMessage("The specified OwnGoalScorerId must be from the opponent team.")
                .When(p => p.OwnGoalScorerId.HasValue);

            RuleFor(p => p)
                .Must(p => !(p.GoalScorerId.HasValue && p.OwnGoalScorerId.HasValue))
                .WithMessage("A goal cannot have both a GoalScorerId and an OwnGoalScorerId at the same time.");

        }
    }
}

using FluentValidation;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Actions.Goals;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.Domain.Enums;
using FantasyFootballGame.DataAccess.Repositories.Actions.Penalties;

public class CreatePenaltyValidator : AbstractValidator<CreatePenaltyDto>
{
    public CreatePenaltyValidator(
        IFixturesRepository fixturesRepository,
        ITeamsRepository teamsRepository,
        IGoalsRepository goalsRepository,
        IPlayersRepository playersRepository,
        IPenaltiesRepository penaltiesRepository)
    {

        RuleFor(x => x.Minute)
            .InclusiveBetween(1, 120).WithMessage("Minute must be between 1 and 120.");


        RuleFor(x => x.FixtureId)
            .MustAsync(async (fixtureId, cancellation) =>
                await fixturesRepository.Exists(f => f.Id == fixtureId))
            .WithMessage("The specified FixtureId does not exist.");

        RuleFor(x => x.TeamId)
            .MustAsync(async (dto, teamId, cancellation) =>
            {
                var fixture = await fixturesRepository.GetById(dto.FixtureId);
                return fixture != null && (fixture.HomeTeamId == teamId || fixture.AwayTeamId == teamId);
            })
            .WithMessage("TeamId must be either the home or away team of the specified fixture.");

        RuleFor(x => x.MissPlayerId)
            .MustAsync(async (dto, missPlayerId, cancellation) =>
            {
                if (!missPlayerId.HasValue) return true;
                var player = await playersRepository.GetById(missPlayerId.Value);
                return player != null && player.TeamId == dto.TeamId;
            })
            .When(x => x.MissPlayerId.HasValue)
            .WithMessage("MissPlayerId must belong to the given team.");

        RuleFor(x => x.SavePlayerId)
            .MustAsync(async (dto, savePlayerId, cancellation) =>
            {
                if (!savePlayerId.HasValue) return true;
                var player = await playersRepository.GetById(savePlayerId.Value);
                return player != null && player.TeamId == dto.TeamId;
            })
            .When(x => x.SavePlayerId.HasValue)
            .WithMessage("SavePlayerId must belong to the given team.");

        RuleFor(x => x.SavePlayerId)
            .MustAsync(async (savePlayerId, cancellation) =>
            {
                if (!savePlayerId.HasValue) return true;
                var player = await playersRepository.GetById(savePlayerId.Value);
                return player != null && player.Position == PlayerPosition.Goalkeeper;
            })
            .When(x => x.SavePlayerId.HasValue)
            .WithMessage("SavePlayerId must be a goalkeeper.");

        RuleFor(x => x.GoalId)
            .NotNull()
            .When(x => x.IsScored)
            .WithMessage("GoalId must be provided when IsScored is true.");

        RuleFor(x => x.GoalId)
            .MustAsync(async (dto, goalId, cancellation) =>
            {
                if (!goalId.HasValue) return true;
                var goal = await goalsRepository.GetById(goalId.Value);
                return goal != null && goal.FixtureId == dto.FixtureId;
            })
            .When(x => x.GoalId.HasValue)
            .WithMessage("GoalId must belong to the same fixture.");

        RuleFor(x => x.GoalId)
            .MustAsync(async (goalId, cancellation) =>
            {
                if (!goalId.HasValue) return true;
                return !await penaltiesRepository.Exists(p => p.GoalId == goalId.Value);
            })
            .When(x => x.GoalId.HasValue)
            .WithMessage("This GoalId is already linked to another penalty.");
    }
}

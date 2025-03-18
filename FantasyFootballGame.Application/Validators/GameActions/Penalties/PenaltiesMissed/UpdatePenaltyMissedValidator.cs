using FluentValidation;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesMissed;
using FantasyFootballGame.DataAccess.Repositories.Actions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesMissed;

public class UpdatePenaltyMissedValidator : AbstractValidator<UpdatePenaltyMissedDto>
{
    public UpdatePenaltyMissedValidator(
        IPlayersRepository playersRepository,
        IPenaltiesRepository penaltiesRepository,
        IPenaltiesMissedRepository penaltiesMissesRepository,
        IFixturesRepository fixturesRepository)
    {

        RuleFor(x => x.PlayerId)
            .MustAsync(async (playerId, cancellation) =>
                !playerId.HasValue || await playersRepository.Exists(p => p.Id == playerId.Value))
            .WithMessage("The specified PlayerId does not exist.");

        RuleFor(x => x.PenaltyId)
            .MustAsync(async (penaltyId, cancellation) =>
                !penaltyId.HasValue || await penaltiesRepository.Exists(p => p.Id == penaltyId.Value))
            .WithMessage("The specified PenaltyId does not exist.");

        RuleFor(x => x)
            .MustAsync(async (dto, cancellation) =>
            {
                if (!dto.PlayerId.HasValue || !dto.PenaltyId.HasValue)
                    return true; 

                var penalty = await penaltiesRepository.GetById(dto.PenaltyId.Value);
                if (penalty == null) return false;

                var fixture = await fixturesRepository.GetById(penalty.FixtureId);
                if (fixture == null) return false;

                return await playersRepository.Exists(p =>
                    p.Id == dto.PlayerId.Value &&
                    (p.TeamId == fixture.HomeTeamId || p.TeamId == fixture.AwayTeamId));
            })
            .WithMessage("The player must belong to one of the two teams in the fixture.");

        RuleFor(x => x)
            .MustAsync(async (dto, cancellation) =>
            {
                if (!dto.PenaltyId.HasValue) return true; 

                var existingMiss = await penaltiesMissesRepository.GetMissedPenaltyByPenaltyId(dto.PenaltyId.Value);
                return existingMiss == null || (dto.PlayerId.HasValue && existingMiss.PlayerId == dto.PlayerId.Value);
            })
            .WithMessage("Another missed penalty record already exists for this penalty.");
    }
}

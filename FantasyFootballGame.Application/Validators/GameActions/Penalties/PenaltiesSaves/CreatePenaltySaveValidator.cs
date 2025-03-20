using FluentValidation;
using FantasyFootballGame.Application.DTOs.GameActions.Penalties.PenaltiesSaves;
using FantasyFootballGame.DataAccess.Repositories.Actions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FantasyFootballGame.Domain.Enums;

public class CreatePenaltySaveValidator : AbstractValidator<CreatePenaltySaveDto>
{
    public CreatePenaltySaveValidator(
        IPlayersRepository playersRepository,
        IPenaltiesRepository penaltiesRepository,
        IFixturesRepository fixturesRepository)
    {
        RuleFor(x => x.PlayerId)
            .MustAsync(async (playerId, cancellation) =>
            {
                var player = await playersRepository.GetById(playerId);
                return player != null && player.Position == PlayerPosition.Goalkeeper;
            })
            .WithMessage("SavePlayerId must be a goalkeeper.");

        RuleFor(x => x.PlayerId)
                .MustAsync(async (dto, playerId, cancellation) =>
                {
                    var penalty = await penaltiesRepository.GetById(dto.PenaltyId);
                    if (penalty == null) return false;

                    var fixture = await fixturesRepository.GetById(penalty.FixtureId);
                    if (fixture == null) return false;

                    var player = await playersRepository.GetById(playerId);
                    if (player == null) return false;

                    return player.TeamId == fixture.HomeTeamId || player.TeamId == fixture.AwayTeamId;
                })
                .WithMessage("The player must belong to one of the two teams in the fixture.");

            RuleFor(x => x.PenaltyId)
                .MustAsync(async (penaltyId, cancellation) =>
                    await penaltiesRepository.Exists(p => p.Id == penaltyId))
                .WithMessage("The specified PenaltyId does not exist.");



            RuleFor(x => x.PenaltyId)
                .MustAsync(async (dto, penaltyId, cancellation) =>
                {
                    return !await penaltiesRepository.Exists(p => p.Id == penaltyId);
                })
                .WithMessage("A missed penalty already exists for this PenaltyId.");
    }
}

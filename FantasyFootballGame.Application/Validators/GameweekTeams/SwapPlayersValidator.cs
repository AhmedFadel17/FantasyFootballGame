using FantasyFootballGame.Application.DTOs.GameweekTeams;
using FantasyFootballGame.Application.Validators.Swaps;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeams;
using FantasyFootballGame.Domain.Enums;
using FluentValidation;


namespace FantasyFootballGame.Application.Validators.GameweekTeams
{
    public class SwapPlayersValidator : AbstractValidator<SwapPlayersDto>
    {
        private int _userId;

        public SwapPlayersValidator(
            IGameweekTeamsRepository gameweekTeamsRepository,
            IGameweekTeamPlayersRepository gameweekTeamPlayersRepository,
            CreateSwapValidator swapValidator)
        {

            RuleFor(t => t.Swaps)
                .NotNull().WithMessage("Swaps list cannot be null.")
                .NotEmpty().WithMessage("At least one swap is required.")
                .ForEach(s => s.SetValidator(swapValidator));

            RuleFor(t => t)
                .MustAsync(async (dto, cancellation) =>
                {
                    var players = await gameweekTeamPlayersRepository.GetByUserId(_userId);

                    var startingPlayers = players.Where(p => p.IsStarting).ToList();
                    var benchPlayers = players.Where(p => !p.IsStarting).ToList();

                    int gkCount = startingPlayers.Count(p => p.Player.Position == PlayerPosition.Goalkeeper);
                    int defCount = startingPlayers.Count(p => p.Player.Position == PlayerPosition.Defender);
                    int midCount = startingPlayers.Count(p => p.Player.Position == PlayerPosition.Midfielder);
                    int fwdCount = startingPlayers.Count(p => p.Player.Position == PlayerPosition.Forward);
                    int benchGKCount = benchPlayers.Count(p => p.Player.Position == PlayerPosition.Goalkeeper);

                    return gkCount == 1 && benchGKCount == 1 &&
                           defCount >= 3 && midCount >= 2 && fwdCount >= 1;
                })
                .WithMessage("Invalid formation: You must have 1 starting GK, 1 bench GK, at least 3 DEFs, 2 MIDs, and 1 FWD.");
        }

        public void SetUserContext(int userId)
        {
            _userId = userId;
        }
    }
}

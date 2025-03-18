using FantasyFootballGame.Application.Interfaces.Players;
using FantasyFootballGame.Application.Interfaces.Teams;
using FantasyFootballGame.Application.Interfaces.FantasyTeams;
using FantasyFootballGame.Application.Interfaces.Fixtures;
using FantasyFootballGame.Application.Interfaces.Gameweeks;
using FantasyFootballGame.Application.Interfaces.GameweekTeams;
using FantasyFootballGame.Application.Interfaces.Transfers;

using FantasyFootballGame.Application.Services.Players;
using FantasyFootballGame.Application.Services.Teams;
using FantasyFootballGame.Application.Services.FantasyTeams;
using FantasyFootballGame.Application.Services.Fixtures;
using FantasyFootballGame.Application.Services.Gameweeks;
using FantasyFootballGame.Application.Services.GameweekTeams;
using FantasyFootballGame.Application.Services.Transfers;

using FantasyFootballGame.Application.Validators.Players;
using FantasyFootballGame.Application.Validators.Teams;
using FantasyFootballGame.Application.Validators.FantasyTeams;
using FantasyFootballGame.Application.Validators.Fixtures;
using FantasyFootballGame.Application.Validators.Gameweeks;
using FantasyFootballGame.Application.Validators.GameweekTeams;
using FantasyFootballGame.Application.Validators.Transfers;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using FantasyFootballGame.Application.Validators.FantasyTeamPlayers;
using FantasyFootballGame.Application.Validators.Swaps;
using FantasyFootballGame.Application.Services.GameActions.Goals;
using FantasyFootballGame.Application.Interfaces.GameActions.Goals;
using FantasyFootballGame.Application.Validators.GameActions.Goals;
using FantasyFootballGame.Application.Validators.GameActions.Goals.Assists;
using FantasyFootballGame.Application.Validators.GameActions.Goals.GoalScored;
using FantasyFootballGame.Application.Validators.GameActions.Goals.OwnGoals;
using FantasyFootballGame.Application.Interfaces.GameActions.Cards;
using FantasyFootballGame.Application.Services.GameActions.Cards;
using FantasyFootballGame.Application.Validators.GameActions.Cards;
using FantasyFootballGame.Application.Interfaces.GameActions.BonusPoints;
using FantasyFootballGame.Application.Services.GameActions.BonusPoints;
using FantasyFootballGame.Application.Validators.GameActions.BonusPoints;
using FantasyFootballGame.Application.Validators.GameActions.BonusPoints.IndividualBonus;
using FantasyFootballGame.Application.Services.GameActions.Injuries;
using FantasyFootballGame.Application.Interfaces.GameActions.Injuries;
using FantasyFootballGame.Application.Validators.GameActions.Injuries;
using FantasyFootballGame.Application.Validators.GameActions.Saves;
using FantasyFootballGame.Application.Interfaces.GameActions.Saves;
using FantasyFootballGame.Application.Services.GameActions.Saves;
using FantasyFootballGame.Application.Interfaces.GameActions.Penalties;
using FantasyFootballGame.Application.Services.GameActions.Penalties;

namespace FantasyFootballGame.Application
{
    public static class Startup
    {
        public static Task<IServiceCollection> AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // ✅ Register All Validators
            services.AddValidatorsFromAssemblyContaining<CreatePlayerValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePlayerValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateTeamValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateTeamValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateFantasyTeamPlayerValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateFantasyTeamPlayerValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateFantasyTeamValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateFantasyTeamValidator>();
            services.AddValidatorsFromAssemblyContaining<MakeTransfersValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateFixtureValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateFixtureValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateGameweekValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateGameweekValidator>();

            // Swaps
            services.AddValidatorsFromAssemblyContaining<SwapPlayersValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateSwapValidator>();

            // Transfers
            services.AddValidatorsFromAssemblyContaining<CreateTransferValidator>();

            // Goals
            services.AddValidatorsFromAssemblyContaining<CreateGoalValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateGoalValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateGoalScoredValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateGoalScoredValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateAssistValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateAssistValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateOwnGoalValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateOwnGoalValidator>();

            // Penalties
            services.AddValidatorsFromAssemblyContaining<CreatePenaltyValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePenaltyValidator>();

            services.AddValidatorsFromAssemblyContaining<CreatePenaltyMissedValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePenaltyMissedValidator>();

            services.AddValidatorsFromAssemblyContaining<CreatePenaltySaveValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdatePenaltySaveValidator>();

            // Cards
            services.AddValidatorsFromAssemblyContaining<CreateCardValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateCardValidator>();

            // Saves
            services.AddValidatorsFromAssemblyContaining<CreateSaveValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateSaveValidator>();

            // Injuries
            services.AddValidatorsFromAssemblyContaining<CreateInjuryValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateInjuryValidator>();

            // Bonus Points
            services.AddValidatorsFromAssemblyContaining<CreateBonusPointsValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateBonusPointsValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateBonusValidator>();


            // ✅ Register All Services
            services.AddScoped<IPlayersService, PlayersService>();
            services.AddScoped<ITeamsService, TeamsService>();
            services.AddScoped<IFantasyTeamsService, FantasyTeamsService>();
            services.AddScoped<IFixturesService, FixturesService>();
            services.AddScoped<IGameweeksService, GameweeksService>();
            services.AddScoped<IGameweekTeamsService, GameweekTeamsService>();
            services.AddScoped<ITransfersService, TransfersService>();
            //Goals
            services.AddScoped<IGoalsService,GoalsService>();
            services.AddScoped<IAssistsService, AssistsService>();
            services.AddScoped<IGoalScoredService,GoalScoredService>();
            services.AddScoped<IOwnGoalsService,OwnGoalsService>();

            // Penalties
            services.AddScoped<IPenaltiesService , PenaltiesService>();
            services.AddScoped<IPenaltiesSavesService , PenaltiesSavesService>();
            services.AddScoped<IPenaltiesMissedService , PenaltiesMissedService>();

            services.AddScoped<ICardsService,CardsService>();
            services.AddScoped<ISavesService, SavesService>();
            services.AddScoped<IInjuriesService, InjuriesService>();
            services.AddScoped<IBonusPointsService, BonusPointsService>();

            return Task.FromResult(services);
        }
    }
}

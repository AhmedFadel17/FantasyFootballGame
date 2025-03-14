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

            services.AddValidatorsFromAssemblyContaining<CreateGameweekTeamValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateGameweekTeamValidator>();

            services.AddValidatorsFromAssemblyContaining<CreateTransferValidator>();


            // ✅ Register All Services
            services.AddScoped<IPlayersService, PlayersService>();
            services.AddScoped<ITeamsService, TeamsService>();
            services.AddScoped<IFantasyTeamsService, FantasyTeamsService>();
            services.AddScoped<IFixturesService, FixturesService>();
            services.AddScoped<IGameweeksService, GameweeksService>();
            services.AddScoped<IGameweekTeamsService, GameweekTeamsService>();
            services.AddScoped<ITransfersService, TransfersService>();

            return Task.FromResult(services);
        }
    }
}

using FantasyFootballGame.Application.Interfaces.Players;
using FantasyFootballGame.Application.Interfaces.Teams;
using FantasyFootballGame.Application.Services.Players;
using FantasyFootballGame.Application.Services.Teams;
using FantasyFootballGame.Application.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyFootballGame.Application
{
    public static class Startup
    {
        public static Task<IServiceCollection> AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddValidatorsFromAssemblyContaining<CreatePlayerValidator>();
            services.AddScoped<ITeamsService, TeamsService>();
            services.AddScoped<IPlayersService, PlayersService>();

            return Task.FromResult(services);
        }
    }
}

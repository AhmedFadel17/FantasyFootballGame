using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.DataAccess.Repositories;
using FantasyFootballGame.DataAccess.Repositories.Actions.Assists;
using FantasyFootballGame.DataAccess.Repositories.Actions.BonusPoints;
using FantasyFootballGame.DataAccess.Repositories.Actions.Goals;
using FantasyFootballGame.DataAccess.Repositories.Actions.GoalsScored;
using FantasyFootballGame.DataAccess.Repositories.Actions.Injuries;
using FantasyFootballGame.DataAccess.Repositories.Actions.OwnGoals;
using FantasyFootballGame.DataAccess.Repositories.Actions.Penalties;
using FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesMissed;
using FantasyFootballGame.DataAccess.Repositories.Actions.PenaltiesSaves;
using FantasyFootballGame.DataAccess.Repositories.Actions.RedCards;
using FantasyFootballGame.DataAccess.Repositories.Actions.Saves;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.FantasyTeams;
using FantasyFootballGame.DataAccess.Repositories.Fixtures;
using FantasyFootballGame.DataAccess.Repositories.GameActions.CleanSheets;
using FantasyFootballGame.DataAccess.Repositories.Gameweeks;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeamPlayers;
using FantasyFootballGame.DataAccess.Repositories.GameweekTeams;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.DataAccess.Repositories.PlayersStats;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FantasyFootballGame.DataAccess.Repositories.Transfers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyFootballGame.DataAccess
{
    public static class Startup
    {
        public static Task<IServiceCollection> AddDataAccessServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<ITeamsRepository, TeamsRepository>();
            services.AddScoped<IPlayersRepository,PlayersRepository>();
            services.AddScoped<IGameweeksRepository,GameweeksRepository>();
            services.AddScoped<IGameweekTeamsRepository,GameweekTeamsRepository>();
            services.AddScoped<IFantasyTeamsRepository,FantasyTeamsRepository>();
            services.AddScoped<IFixturesRepository,FixturesRepository>();
            services.AddScoped<IPlayersStatsRepository,PlayersStatsRepository>();
            services.AddScoped<ITransfersRepository,TransfersRepository>();
            services.AddScoped<IAssistsRepository,AssistsRepository>();
            services.AddScoped<IGoalsRepository,GoalsRepository>();
            services.AddScoped<IGoalsScoredRepository,GoalsScoredRepository>();
            services.AddScoped<IOwnGoalsRepository,OwnGoalsRepository>();
            services.AddScoped<IBonusPointsRepository,BonusPointsRepository>();
            services.AddScoped<IInjuriesRepository,InjuriesRepository>();
            services.AddScoped<IPenaltiesRepository,PenaltiesRepository>();
            services.AddScoped<IPenaltiesMissedRepository,PenaltiesMissedRepository>();
            services.AddScoped<IPenaltiesSavesRepository,PenaltiesSavesRepository>();
            services.AddScoped<ICardsRepository,CardsRepository>();
            services.AddScoped<ISavesRepository,SavesRepository>();
            services.AddScoped<ICleanSheetsRepository,CleanSheetsRepository>();
            services.AddScoped<IFanatsyTeamPlayersRepository,FantasyTeamPlayerRepository>();
            services.AddScoped<IGameweekTeamPlayersRepository,GameweekTeamPlayersRepository>();

            return Task.FromResult(services);
        }

    }
}

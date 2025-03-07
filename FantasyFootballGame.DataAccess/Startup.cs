using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.DataAccess.Repositories;
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
            return Task.FromResult(services);
        }

    }
}

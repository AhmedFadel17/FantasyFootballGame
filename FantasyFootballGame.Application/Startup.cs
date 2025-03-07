using Microsoft.Extensions.DependencyInjection;

namespace FantasyFootballGame.Application
{
    public static class Startup
    {
        public static Task<IServiceCollection> AddApplicationServices(this IServiceCollection services)
        {
            return Task.FromResult(services);
        }
    }
}

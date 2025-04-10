using FantasyFootballGame.DataAccess.Data;
using FantasyFootballGame.Domain.Settings;
using FantasyFootballGame.IntegrationTests.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Caching.Memory;
using FantasyFootballGame.API.Configurations;

namespace FantasyFootballGame.IntegrationTests
{
    public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
            builder.ConfigureServices(services =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var conf = serviceProvider.GetRequiredService<IConfiguration>();
                var identitySettings = conf.GetJsonSection<IdentityServerSettings>("IdentityServerSettings");
                services.AddMemoryCache();

                services.AddSingleton<IAuthService>(sp =>
                {
                    var memoryCache = sp.GetRequiredService<IMemoryCache>();
                    return new AuthService(identitySettings, memoryCache);
                }); var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
                if (descriptor != null)
                    services.Remove(descriptor);

                services
                .AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<AppDbContext>((sp, options) =>
                {
                    options.UseInMemoryDatabase("TestDb").UseInternalServiceProvider(sp);
                });

            });
        }
    }
}

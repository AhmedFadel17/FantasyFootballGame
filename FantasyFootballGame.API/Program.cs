using FantasyFootballGame.DataAccess;
using FantasyFootballGame.Application;
using FantasyFootballGame.Domain.Settings;
using FantasyFootballGame.API.Configurations;
using FantasyFootballGame.API.Middlewares;
using FantasyFootballGame.DataAccess.Seeds;
using FantasyFootballGame.DataAccess.Repositories.Players;
using FantasyFootballGame.DataAccess.Repositories.Teams;
using FantasyFootballGame.API.Filters;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true; // Prevent ASP.NET from overriding validation errors
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<ModelValidationFilter>(); // Add the filter globally
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
await builder.Services.AddDataAccessServices(builder.Configuration);
await builder.Services.AddApplicationServices();
var identitySettings = builder.Configuration.GetJsonSection<IdentityServerSettings>("IdentityServerSettings");
builder.Services.AddSingleton<IdentityServerSettings>(identitySettings);
builder.Services.AddCustomAuthentication(identitySettings);
var app = builder.Build();

// Check for seeding command
if (args.Length > 0 && args[0].ToLower() == "seed")
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var teamsRepo = services.GetRequiredService<ITeamsRepository>();
            var playersRepo = services.GetRequiredService<IPlayersRepository>();
            
            Console.WriteLine("Starting to seed LaLiga players...");
            var seeder = new LaLigaPlayerSeeder(teamsRepo, playersRepo);
            await seeder.SeedAsync();
            Console.WriteLine("LaLiga players seeded successfully!");
            
            // Exit after seeding
            return;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
            return;
        }
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "openapi/v1.json";
    });

    // Seed data in development
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var teamsRepo = services.GetRequiredService<ITeamsRepository>();
            var playersRepo = services.GetRequiredService<IPlayersRepository>();
            
            Console.WriteLine("Starting to seed LaLiga players...");
            var seeder = new LaLigaPlayerSeeder(teamsRepo, playersRepo);
            await seeder.SeedAsync();
            Console.WriteLine("LaLiga players seeded successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while seeding the database: {ex.Message}");
        }
    }
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseRouting(); 
//app.UseMiddleware<BadRequestMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>(); 

app.UseAuthorization();
app.MapControllers();
app.Run();

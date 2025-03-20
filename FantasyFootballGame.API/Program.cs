using FantasyFootballGame.DataAccess;
using FantasyFootballGame.Application;
using FantasyFootballGame.Domain.Settings;
using Microsoft.IdentityModel.Tokens;
using FantasyFootballGame.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
await builder.Services.AddDataAccessServices(builder.Configuration);
await builder.Services.AddApplicationServices();
var identitySettings = builder.Configuration.GetJsonSection<IdentityServerSettings>("IdentityServerSettings");
builder.Services.AddSingleton<IdentityServerSettings>(identitySettings);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = identitySettings.Authority;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = identitySettings.Authority,
            ValidateAudience = false,
            ValidAudience = identitySettings.ClientId,
            ValidateLifetime = true,

            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            NameClaimType = "name"

        };
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

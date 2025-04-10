using FantasyFootballGame.Application.DTOs.Common;
using FantasyFootballGame.Domain.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace FantasyFootballGame.API.Configurations
{
    public static class ServiceProviderExtensions
    {
        public static T GetJsonSection<T>(this IConfiguration configuration, string key)
        {
            var valueObject = configuration.GetSection(key).Get<T>();

            // try to get configuration from environment variable
            if (valueObject is null)

                return JsonSerializer.Deserialize<T>(configuration.GetSection(key)?.Value);

            return valueObject;
        }

        public static void AddCustomAuthentication(this IServiceCollection services, IdentityServerSettings identitySettings)
        {
            services.AddAuthentication("Bearer")
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

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            context.Response.ContentType = "application/json";

                            var errorResponse = new ErrorResponseDto
                            {
                                Message = "Unauthorized: Invalid or missing token.",
                                StatusCode = StatusCodes.Status401Unauthorized
                            };

                            await context.Response.WriteAsJsonAsync(errorResponse);
                        },
                        OnForbidden = async context =>
                        {
                            context.Response.StatusCode = StatusCodes.Status403Forbidden;
                            context.Response.ContentType = "application/json";

                            var errorResponse = new ErrorResponseDto
                            {
                                Message = "Forbidden: You do not have permission to access this resource.",
                                StatusCode = StatusCodes.Status403Forbidden
                            };

                            await context.Response.WriteAsJsonAsync(errorResponse);
                        }
                    };
                });
        }
    }
}

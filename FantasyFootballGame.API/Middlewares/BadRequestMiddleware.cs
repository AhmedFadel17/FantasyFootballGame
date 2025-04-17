using FantasyFootballGame.API.Factories;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FantasyFootballGame.API.Middlewares
{
    public class BadRequestMiddleware
    {
        private readonly RequestDelegate _next;

        public BadRequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            if (context.Response.StatusCode == StatusCodes.Status400BadRequest)
            {
                context.Response.ContentType = "application/json";
                var errors = new Dictionary<string, List<string>>();

                // Try to retrieve ModelState from the request
                if (context.Items["ModelState"] is ModelStateDictionary modelState)
                {
                    foreach (var entry in modelState)
                    {
                        var errorMessages = entry.Value.Errors.Select(e => e.ErrorMessage).ToList();
                        if (errorMessages.Any())
                        {
                            errors[entry.Key] = errorMessages;
                        }
                    }
                }

                var response = ApiResponseFactory.Error("One or more validation errors occurred.", errors, StatusCodes.Status400BadRequest);

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }
}

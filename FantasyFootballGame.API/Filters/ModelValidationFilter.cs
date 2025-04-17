using FantasyFootballGame.API.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace FantasyFootballGame.API.Filters
{
    public class ModelValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = new Dictionary<string, List<string>>();

                foreach (var entry in context.ModelState)
                {
                    var fieldName = entry.Key;
                    var errorMessages = entry.Value.Errors
                        .Select(e => e.ErrorMessage)
                        .Where(msg => !string.IsNullOrWhiteSpace(msg))
                        .ToList();

                    if (errorMessages.Any())
                    {
                        errors[fieldName] = errorMessages;
                    }
                }
                var response = ApiResponseFactory.Error("Validation failed.", errors);

                context.Result = new JsonResult(response)
                {
                    StatusCode = 400
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}

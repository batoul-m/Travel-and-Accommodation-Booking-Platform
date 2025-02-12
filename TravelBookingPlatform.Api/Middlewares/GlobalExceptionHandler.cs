using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;

namespace TravelBookingPlatform.Api.Middlewares
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, Exception exception,
            CancellationToken cancellationToken)
        {
            LogException(exception);

            var (statusCode, title, detail) = MapExceptionToProblemInformation(exception);

            await Results.Problem(
                statusCode: statusCode,
                title: title,
                detail: detail,
                extensions: new Dictionary<string, object?>
                {
                    ["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier
                }).ExecuteAsync(httpContext);

            return true;
        }

        private void LogException(Exception exception)
        {
            if (exception is UnauthorizedAccessException || exception is ArgumentException)
            {
                _logger.LogWarning(exception, exception.Message);
            }
            else
            {
                _logger.LogError(exception, exception.Message);
            }
        }

        private static (int statusCode, string title, string detail) MapExceptionToProblemInformation(Exception exception)
        {
            return exception switch
            {
                KeyNotFoundException => (
                    StatusCodes.Status404NotFound,
                    "Resource Not Found",
                    "The requested resource could not be found."
                ),
                UnauthorizedAccessException => (
                    StatusCodes.Status401Unauthorized,
                    "Unauthorized",
                    "You are not authorized to access this resource."
                ),
                ArgumentException => (
                    StatusCodes.Status400BadRequest,
                    "Bad Request",
                    "The request could not be understood or was missing required parameters."
                ),
                InvalidOperationException => (
                    StatusCodes.Status409Conflict,
                    "Conflict",
                    "There was a conflict with the current state of the resource."
                ),
                _ => (
                    StatusCodes.Status500InternalServerError,
                    "Internal Server Error",
                    "An unexpected error occurred on the server."
                )
            };
        }
    }
}

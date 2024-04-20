using Microsoft.AspNetCore.Diagnostics;

namespace KarmaMarketplace.Presentation.Web.ExceptionHandlers
{
    public class GuardClauseExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GuardClauseExceptionHandler> _logger;

        public GuardClauseExceptionHandler(ILogger<GuardClauseExceptionHandler> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            // Handle GuardClauseViolationException
            if (exception is ArgumentNullException guardException)
            {
                LogGuardClauseViolationException(guardException);
                return HandleGuardClauseViolationAsync(httpContext, guardException.Message);
            }

            // Handle other exceptions within your namespace
            if (IsInYourNamespace(exception))
            {
                LogExceptionInYourNamespace(exception);
                return HandleExceptionInYourNamespaceAsync(httpContext, exception);
            }

            // Return false to indicate that this exception is not handled by this handler
            return ValueTask.FromResult(false);
        }

        private void LogGuardClauseViolationException(ArgumentNullException guardException)
        {
            _logger.LogError(guardException, $"Guard clause violation: {guardException.Message}");
        }

        private async ValueTask<bool> HandleGuardClauseViolationAsync(HttpContext httpContext, string errorMessage)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errorResponse = new
            {
                ErrorMessage = errorMessage
            };

            await httpContext.Response.WriteAsJsonAsync(errorResponse);

            // Return true to indicate that this exception is handled by this handler
            return true;
        }

        private bool IsInYourNamespace(Exception exception)
        {
            // Check if the exception type is within your namespace
            return exception.TargetSite?.DeclaringType?.Namespace?.StartsWith("YourNamespace") == true;
        }

        private void LogExceptionInYourNamespace(Exception exception)
        {
            // Log the exception with the last method in the stack trace within your namespace
            var stackTrace = exception.StackTrace?.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            if (stackTrace != null && stackTrace.Any())
            {
                var lastNamespaceMethod = stackTrace.FirstOrDefault(line => line.Contains("KarmaMarketplace"));
                if (!string.IsNullOrWhiteSpace(lastNamespaceMethod))
                {
                    _logger.LogError(exception, $"Exception in your namespace: {exception.Message}. Last method in stack trace: {lastNamespaceMethod}");
                    return;
                }
            }

            // If no method in the stack trace is within your namespace, log the exception with its message
            _logger.LogError(exception, $"Exception in your namespace: {exception.Message}");
        }

        private async ValueTask<bool> HandleExceptionInYourNamespaceAsync(HttpContext httpContext, Exception exception)
        {
            // Customize the response for exceptions within your namespace
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var errorResponse = new
            {
                ErrorMessage = "An error occurred within your namespace."
            };

            await httpContext.Response.WriteAsJsonAsync(errorResponse);

            // Return true to indicate that this exception is handled by this handler
            return true;
        }
    }
}

using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace DoctorAppointment.API.ExceptionHandlers
{
    public class ValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not ValidationException validation)
            {
                return false;
            }

            int statusCode = StatusCodes.Status400BadRequest;

            HttpValidationProblemDetails validationProblem = new()
            {
                Status = statusCode,
                Instance = httpContext.Request.Path,
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Errors = new Dictionary<string, string[]> { { "validation", validation.Errors.Select(error => error.ErrorMessage).ToArray() } }
            };

            httpContext.Response.StatusCode = statusCode;

            await httpContext.Response.WriteAsJsonAsync(validationProblem, cancellationToken);
            return true;
        }
    }
}

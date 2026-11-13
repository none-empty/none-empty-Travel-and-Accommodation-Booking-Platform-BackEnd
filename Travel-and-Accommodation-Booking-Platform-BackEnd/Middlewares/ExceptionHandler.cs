using System.Security.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Travel_and_Accommodation_Booking_Platform_BackEnd.Application.Common.Exceptions;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Middlewares;

public class ExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (status, title, detail) = exception switch
        {
            ValidationException => (
                StatusCodes.Status400BadRequest,
                "One or more validation errors occurred.",
                "The request contains invalid values."),
            InvalidCredentialException => (
                StatusCodes.Status401Unauthorized,
                "Authentication failed.",
                "The supplied credentials are invalid or have expired."),
            BadRequestException badRequest => (
                StatusCodes.Status400BadRequest,
                "Invalid request.",
                badRequest.PublicMessage),
            ConflictException conflict => (
                StatusCodes.Status409Conflict,
                "Request conflict.",
                conflict.PublicMessage),
            _ => (
                StatusCodes.Status500InternalServerError,
                "An unexpected error occurred.",
                "Please try again later.")
        };

        ProblemDetails problem = exception is ValidationException validation
            ? new HttpValidationProblemDetails(validation.Errors
                .GroupBy(error => error.PropertyName)
                .ToDictionary(group => group.Key,
                    group => group.Select(error => error.ErrorMessage).Distinct().ToArray()))
            : new ProblemDetails();

        if (exception is ConflictException { Conflicts: not null } bookingConflict)
            problem.Extensions["conflicts"] = bookingConflict.Conflicts;

        problem.Status = status;
        problem.Type = "about:blank";
        problem.Title = title;
        problem.Detail = detail;
        problem.Extensions["requestId"] = httpContext.TraceIdentifier;

        httpContext.Response.StatusCode = status;
        if (status == StatusCodes.Status401Unauthorized)
            httpContext.Response.Headers.WWWAuthenticate = "Bearer";

        // ASP.NET Core 9's exception middleware logs the original exception.
        // Only this explicitly constructed public response is sent to the client.
        if (!await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem
            }))
        {
            // A writer may decline an unsupported Accept header. Still finish
            // handling the exception, without falling through to a developer page.
            await httpContext.Response.WriteAsJsonAsync(problem, problem.GetType(),
                options: null, contentType: "application/problem+json", cancellationToken);
        }

        return true;
    }
}

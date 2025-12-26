using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Travel_and_Accommodation_Booking_Platform_BackEnd.Middlewares;

public class ExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = exception switch
        {
          _ => StatusCodes.Status500InternalServerError
        };

        return await problemDetailsService.TryWriteAsync( new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Type = exception.GetType().Name,
                    Title = "an error occured",
                    Detail = exception.Message
                }
            }
            
        );
    }
}
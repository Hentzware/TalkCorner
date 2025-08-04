using System.Net;
using System.Security.Authentication;
using TalkCorner.API.Models;
using TalkCorner.Application.Exceptions;

namespace TalkCorner.API.Middleware;

public class ExceptionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = HttpStatusCode.InternalServerError;
        CustomProblemDetails problem;

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                };
                break;

            case NotFoundException notFoundException:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Detail = notFoundException.InnerException?.Message,
                    Type = nameof(NotFoundException)
                };
                break;

            case UnauthorizedException unauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                problem = new CustomProblemDetails
                {
                    Title = unauthorizedException.Message,
                    Status = (int)statusCode,
                    Detail = unauthorizedException.InnerException?.Message,
                    Type = nameof(UnauthorizedException)
                };
                break;

            case ForbiddenException forbiddenException:
                statusCode = HttpStatusCode.Forbidden;
                problem = new CustomProblemDetails
                {
                    Title = forbiddenException.Message,
                    Status = (int)statusCode,
                    Detail = forbiddenException.InnerException?.Message,
                    Type = nameof(ForbiddenException)
                };
                break;

            case ServiceUnavailableException serviceUnavailableException:
                statusCode = HttpStatusCode.ServiceUnavailable;
                problem = new CustomProblemDetails
                {
                    Title = serviceUnavailableException.Message,
                    Status = (int)statusCode,
                    Detail = serviceUnavailableException.InnerException?.Message,
                    Type = nameof(ServiceUnavailableException)
                };
                break;

            case InvalidCredentialException invalidCredentialsException:
                statusCode = HttpStatusCode.Unauthorized;
                problem = new CustomProblemDetails
                {
                    Title = invalidCredentialsException.Message,
                    Status = (int)statusCode,
                    Detail = invalidCredentialsException.InnerException?.Message,
                    Type = nameof(InvalidCredentialException)
                };
                break;

            default:
                problem = new CustomProblemDetails
                {
                    Title = "An unexpected error occurred!",
                    Status = (int)statusCode,
                    Detail = exception.Message,
                    Type = "InternalServerError"
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}
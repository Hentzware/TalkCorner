using System.Net;
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
        var problem = new CustomValidationProblemDetails();

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomValidationProblemDetails
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
                problem = new CustomValidationProblemDetails
                {
                    Title = notFoundException.Message,
                    Status = (int)statusCode,
                    Detail = notFoundException.InnerException?.Message,
                    Type = nameof(NotFoundException)
                };
                break;

            case ConflictException conflictException:
                statusCode = HttpStatusCode.Conflict;
                problem = new CustomValidationProblemDetails
                {
                    Title = conflictException.Message,
                    Status = (int)statusCode,
                    Detail = conflictException.InnerException?.Message,
                    Type = nameof(ConflictException)
                };
                break;

            case UnauthorizedException unauthorizedException:
                statusCode = HttpStatusCode.Unauthorized;
                problem = new CustomValidationProblemDetails
                {
                    Title = unauthorizedException.Message,
                    Status = (int)statusCode,
                    Detail = unauthorizedException.InnerException?.Message,
                    Type = nameof(UnauthorizedException)
                };
                break;

            case ForbiddenException forbiddenException:
                statusCode = HttpStatusCode.Forbidden;
                problem = new CustomValidationProblemDetails
                {
                    Title = forbiddenException.Message,
                    Status = (int)statusCode,
                    Detail = forbiddenException.InnerException?.Message,
                    Type = nameof(ForbiddenException)
                };
                break;

            case UnprocessableEntityException unprocessableEntityException:
                statusCode = (HttpStatusCode)422; // Unprocessable Entity
                problem = new CustomValidationProblemDetails
                {
                    Title = unprocessableEntityException.Message,
                    Status = (int)statusCode,
                    Detail = unprocessableEntityException.InnerException?.Message,
                    Type = nameof(UnprocessableEntityException),
                    Errors = unprocessableEntityException.Errors
                };
                break;

            case ServiceUnavailableException serviceUnavailableException:
                statusCode = HttpStatusCode.ServiceUnavailable;
                problem = new CustomValidationProblemDetails
                {
                    Title = serviceUnavailableException.Message,
                    Status = (int)statusCode,
                    Detail = serviceUnavailableException.InnerException?.Message,
                    Type = nameof(ServiceUnavailableException)
                };
                break;

            default:
                problem = new CustomValidationProblemDetails
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
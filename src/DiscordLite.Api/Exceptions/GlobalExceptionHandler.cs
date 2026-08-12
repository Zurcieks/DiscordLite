using DiscordLite.Application.Exceptions;
using DiscordLite.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DiscordLite.Api.Exceptions;

public sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            NotFoundException =>
                (StatusCodes.Status404NotFound, "Not Found"),

            ForbiddenException =>
                (StatusCodes.Status403Forbidden, "Forbidden"),

            ConflictException =>
                (StatusCodes.Status409Conflict, "Conflict"),

            BadRequestException =>
                (StatusCodes.Status400BadRequest, "Bad Request"),

            UnauthorizedException =>
                (StatusCodes.Status401Unauthorized, "Unauthorized"),

            ValidationRequestException =>
                (StatusCodes.Status400BadRequest, "Validation Error"),
            
            DomainValidationException => 
                (StatusCodes.Status400BadRequest, "Validation Error"),
            
            DomainForbiddenException =>
                (StatusCodes.Status403Forbidden, "Forbidden"),
            
            DomainConflictException =>
                (StatusCodes.Status409Conflict, "Conflict"),
            
            DomainException =>
                (StatusCodes.Status422UnprocessableEntity,
                    "Business Rule Violation"),
            
            

            _ =>
                (StatusCodes.Status500InternalServerError,
                    "Internal Server Error")
        };

        if (statusCode >= 500)
        {
            logger.LogError(
                exception,
                "Unhandled exception. TraceId: {TraceId}",
                httpContext.TraceIdentifier);
        }
        else
        {
            logger.LogInformation(
                "Request rejected with {ExceptionType}. TraceId: {TraceId}",
                exception.GetType().Name,
                httpContext.TraceIdentifier);
        }

        var detail = statusCode >= 500
            ? "An unexpected error occurred."
            : exception.Message;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions["traceId"] =
            httpContext.TraceIdentifier;

        if (exception is ValidationRequestException validationException)
        {
            problemDetails.Extensions["errors"] =
                validationException.Errors;
        }

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            problemDetails,
            cancellationToken);

        return true;
    }
}
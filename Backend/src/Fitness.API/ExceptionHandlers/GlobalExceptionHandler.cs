using System.Diagnostics;
using System.Text.Json;
using Fitness.API.Responses;
using Fitness.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace Fitness.API.ExceptionHandlers;

public sealed class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(
        HttpContext context,
        Exception exception,
        CancellationToken cancellationToken)
    {
        _logger.LogError(exception, exception.Message);

        var statusCode = exception switch
        {
            InvalidOtpException => StatusCodes.Status400BadRequest,
            OtpExpiredException => StatusCodes.Status400BadRequest,
            TooManyOtpAttemptsException => StatusCodes.Status429TooManyRequests,
            OtpAlreadySentException => StatusCodes.Status429TooManyRequests,
            _ => StatusCodes.Status500InternalServerError
        };

        var code = exception switch
        {
            AppException ex => ex.ErrorCode,
            _ => "INTERNAL_SERVER_ERROR"
        };

        var message = exception switch
        {
            AppException ex => ex.Message,
            _ => "خطای داخلی سرور رخ داده است."
        };

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var response = new ErrorResponse
        {
            Code = code,
            Message = message,
            TraceId = Activity.Current?.Id ?? context.TraceIdentifier
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response),
            cancellationToken);

        return true;
    }
}
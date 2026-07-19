namespace Fitness.API.Responses;

public sealed class ErrorResponse
{
    public bool Success => false;

    public string Code { get; init; } = string.Empty;

    public string Message { get; init; } = string.Empty;

    public string? TraceId { get; init; }
}
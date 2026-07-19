public interface IOtpService
{
    Task SendOtpAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default);

    Task VerifyOtpAsync(
        string phoneNumber,
        string code,
        CancellationToken cancellationToken = default);
}
using Fitness.Application.DTOs.Auth;

namespace Fitness.Application.Interfaces;

public interface IAuthService
{
    Task SendOtpAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default);

    Task<LoginResponse> VerifyOtpAsync(
        string phoneNumber,
        string code,
        CancellationToken cancellationToken = default);

    Task<LoginResponse> GuestLoginAsync(
        CancellationToken cancellationToken = default);
        
        Task<LoginResponse> RefreshTokenAsync(
    string refreshToken,
    CancellationToken cancellationToken = default);
    Task LogoutAsync(
    string refreshToken,
    CancellationToken cancellationToken = default);
}
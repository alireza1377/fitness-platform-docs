using Fitness.Application.DTOs.Auth;

namespace Fitness.Application.Interfaces;

public interface IAuthService
{
    Task<LoginResponse> VerifyOtpAsync(
        VerifyOtpRequest request,
        CancellationToken cancellationToken = default);
}
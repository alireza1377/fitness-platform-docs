using Fitness.Application.DTOs.Profile;

namespace Fitness.Application.Interfaces;

public interface IProfileService
{
    Task<ProfileResponse> GetProfileAsync(
        CancellationToken cancellationToken = default);

    Task UpdateProfileAsync(
        UpdateProfileRequest request,
        CancellationToken cancellationToken = default);
}
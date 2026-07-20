using Fitness.Application.DTOs.Profile;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Services;

public class ProfileService : IProfileService
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserService _currentUserService;

    public ProfileService(
        IUserRepository userRepository,
        ICurrentUserService currentUserService)
    {
        _userRepository = userRepository;
        _currentUserService = currentUserService;
    }

    public async Task<ProfileResponse> GetProfileAsync(
        CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId is null)
            throw new UnauthorizedAccessException();

        var user = await _userRepository.GetByIdAsync(
            _currentUserService.UserId.Value,
            cancellationToken);

        if (user is null)
            throw new Exception("User not found.");

        return new ProfileResponse
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            AvatarUrl = user.AvatarUrl,
            BirthDate = user.BirthDate,
            Gender = user.Gender,
            Height = user.Height,
            Weight = user.Weight,
            Goal = user.Goal,
            ActivityLevel = user.ActivityLevel
        };
    }

    public async Task UpdateProfileAsync(
        UpdateProfileRequest request,
        CancellationToken cancellationToken = default)
    {
        if (!_currentUserService.IsAuthenticated || _currentUserService.UserId is null)
            throw new UnauthorizedAccessException();

        var user = await _userRepository.GetByIdAsync(
            _currentUserService.UserId.Value,
            cancellationToken);

        if (user is null)
            throw new Exception("User not found.");

        user.UpdateProfile(
            request.FirstName,
            request.LastName,
            request.Email,
            request.AvatarUrl,
            request.BirthDate,
            request.Gender,
            request.Height,
            request.Weight,
            request.Goal,
            request.ActivityLevel);

        _userRepository.Update(user);

        await _userRepository.SaveChangesAsync(cancellationToken);
    }
}
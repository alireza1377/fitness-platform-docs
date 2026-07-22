using Fitness.Application.DTOs.Dashboard;
using Fitness.Application.Interfaces;

namespace Fitness.Application.Dashboard.Builders;

public class UserCardBuilder
{
    private readonly IUserRepository _userRepository;

    public UserCardBuilder(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserCardDto> BuildAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var user =
            await _userRepository.GetByIdAsync(
                userId,
                cancellationToken);

        if (user is null)
            throw new Exception("User not found.");

        return new UserCardDto
        {
            UserId = user.Id,
            FullName = $"{user.FirstName} {user.LastName}".Trim(),
            ProfileImageUrl = user.AvatarUrl,
            Level = "Beginner"
        };
    }
}
using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();
    string GenerateGuestAccessToken();
}
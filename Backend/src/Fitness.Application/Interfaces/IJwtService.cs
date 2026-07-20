using System.Security.Claims;
using Fitness.Domain.Entities;

namespace Fitness.Application.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(User user);

    string GenerateGuestAccessToken();

    string GenerateRefreshToken();

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}
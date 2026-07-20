using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Fitness.Infrastructure.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid? UserId
    {
        get
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user is null)
                return null;

            // برای دیباگ
            Console.WriteLine("========== JWT CLAIMS ==========");
            foreach (var claim in user.Claims)
            {
                Console.WriteLine($"{claim.Type} = {claim.Value}");
            }
            Console.WriteLine("================================");

            var value = user.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(value))
            {
                value = user.FindFirstValue(JwtRegisteredClaimNames.Sub);
            }

            if (Guid.TryParse(value, out var id))
                return id;

            return null;
        }
    }

    public bool IsGuest =>
        _httpContextAccessor.HttpContext?
            .User?
            .FindFirst("isGuest")?
            .Value == "true";

    public bool IsAuthenticated =>
        _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
}
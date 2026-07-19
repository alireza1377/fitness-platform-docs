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
            var value = _httpContextAccessor.HttpContext?
                .User?
                .FindFirstValue(ClaimTypes.NameIdentifier);

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
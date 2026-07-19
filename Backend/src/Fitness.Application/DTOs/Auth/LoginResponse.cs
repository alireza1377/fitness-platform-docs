namespace Fitness.Application.DTOs.Auth;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;

    public string RefreshToken { get; set; } = string.Empty;

    public int ExpiresIn { get; set; }

    public UserDto User { get; set; } = default!;
}

public class UserDto
{
    public Guid Id { get; set; }

    public string PhoneNumber { get; set; } = string.Empty;

    public bool IsGuest { get; set; }
}
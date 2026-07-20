using Fitness.Application.DTOs.Auth;
using Fitness.Application.Interfaces;
using Fitness.Domain.Entities;
using Fitness.Shared.Security;

namespace Fitness.Application.Services;

public class AuthService : IAuthService
{
    private readonly IOtpService _otpService;
    private readonly IUserRepository _userRepository;
    private readonly IAuthIdentityRepository _authIdentityRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IJwtService _jwtService;

    public AuthService(
        IOtpService otpService,
        IUserRepository userRepository,
        IAuthIdentityRepository authIdentityRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IJwtService jwtService)
    {
        _otpService = otpService;
        _userRepository = userRepository;
        _authIdentityRepository = authIdentityRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _jwtService = jwtService;
    }


    public async Task SendOtpAsync(
        string phoneNumber,
        CancellationToken cancellationToken = default)
    {
        await _otpService.SendOtpAsync(
            phoneNumber,
            cancellationToken);
    }


    public async Task<LoginResponse> VerifyOtpAsync(
        string phoneNumber,
        string code,
        CancellationToken cancellationToken = default)
    {
        // Verify OTP
        await _otpService.VerifyOtpAsync(
            phoneNumber,
            code,
            cancellationToken);


        // Find User
        var user = await _userRepository.GetByPhoneNumberAsync(
            phoneNumber,
            cancellationToken);


        // First Login
        if (user is null)
        {
            user = new User(phoneNumber);

            user.Verify();


            await _userRepository.AddAsync(
                user,
                cancellationToken);


            await _userRepository.SaveChangesAsync(
                cancellationToken);


            var authIdentity = new AuthIdentity(user.Id);

            authIdentity.VerifyPhone();

            authIdentity.LoginSucceeded();


            await _authIdentityRepository.AddAsync(
                authIdentity,
                cancellationToken);


            await _authIdentityRepository.SaveChangesAsync(
                cancellationToken);
        }
        else
        {
            user.Verify();

            user.SetLastLogin();


            await _userRepository.SaveChangesAsync(
                cancellationToken);
        }



        // Generate JWT
        var accessToken =
            _jwtService.GenerateAccessToken(user);


        var refreshToken =
            _jwtService.GenerateRefreshToken();



        // Store Refresh Token
        var refreshTokenEntity = new RefreshToken(
            user.Id,
            RefreshTokenHasher.Hash(refreshToken),
            Guid.NewGuid().ToString(),
            DateTime.UtcNow.AddDays(30));


        await _refreshTokenRepository.AddAsync(
            refreshTokenEntity,
            cancellationToken);


        await _refreshTokenRepository.SaveChangesAsync(
            cancellationToken);



        return new LoginResponse
        {
            AccessToken = accessToken,

            RefreshToken = refreshToken,

            ExpiresIn = 60 * 60,

            User = new UserDto
            {
                Id = user.Id,

                PhoneNumber = user.PhoneNumber,

                IsGuest = false
            }
        };
    }



   public async Task<LoginResponse> RefreshTokenAsync(
    string refreshToken,
    CancellationToken cancellationToken = default)
{
    var hash = RefreshTokenHasher.Hash(refreshToken);

    var storedToken =
        await _refreshTokenRepository.GetByTokenHashAsync(
            hash,
            cancellationToken);

    if (storedToken is null)
        throw new Exception("Refresh token is invalid.");

    if (storedToken.IsExpired())
        throw new Exception("Refresh token expired.");

    if (storedToken.IsRevoked())
        throw new Exception("Refresh token revoked.");

    storedToken.Revoke();

    _refreshTokenRepository.Update(storedToken);

    var user = storedToken.User;

    var newAccessToken =
        _jwtService.GenerateAccessToken(user);

    var newRefreshToken =
        _jwtService.GenerateRefreshToken();

    var newEntity = new RefreshToken(
        user.Id,
        RefreshTokenHasher.Hash(newRefreshToken),
        Guid.NewGuid().ToString(),
        DateTime.UtcNow.AddDays(30));

    await _refreshTokenRepository.AddAsync(
        newEntity,
        cancellationToken);

    await _refreshTokenRepository.SaveChangesAsync(
        cancellationToken);

    return new LoginResponse
    {
        AccessToken = newAccessToken,
        RefreshToken = newRefreshToken,
        ExpiresIn = 3600,
        User = new UserDto
        {
            Id = user.Id,
            PhoneNumber = user.PhoneNumber,
            IsGuest = false
        }
    };
}



    public Task<LoginResponse> GuestLoginAsync(
        CancellationToken cancellationToken = default)
    {
        var accessToken =
            _jwtService.GenerateGuestAccessToken();


        return Task.FromResult(
            new LoginResponse
            {
                AccessToken = accessToken,

                RefreshToken = string.Empty,

                ExpiresIn = 60 * 60,


                User = new UserDto
                {
                    Id = Guid.Empty,

                    PhoneNumber = string.Empty,

                    IsGuest = true
                }
            });
    }
    public async Task LogoutAsync(
    string refreshToken,
    CancellationToken cancellationToken = default)
{
    var hash = RefreshTokenHasher.Hash(refreshToken);

    var token = await _refreshTokenRepository.GetByTokenHashAsync(
        hash,
        cancellationToken);

    if (token is null)
        return;

    if (!token.IsRevoked())
    {
        token.Revoke();

        _refreshTokenRepository.Update(token);

        await _refreshTokenRepository.SaveChangesAsync(
            cancellationToken);
    }
}
}
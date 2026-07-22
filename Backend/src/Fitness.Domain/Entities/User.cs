using Fitness.Domain.Common;
using Fitness.Domain.Enums;

namespace Fitness.Domain.Entities;

public class User : AuditableEntity
{
    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string? Email { get; private set; }

    public string PhoneNumber { get; private set; } = string.Empty;

    public string? AvatarUrl { get; private set; }

    public DateOnly? BirthDate { get; private set; }

    public Gender? Gender { get; private set; }

    public decimal? Height { get; private set; }

    public decimal? Weight { get; private set; }

    public FitnessGoal? Goal { get; private set; }

    public ActivityLevel? ActivityLevel { get; private set; }

    public bool IsActive { get; private set; } = true;

    public bool IsVerified { get; private set; } = false;

    public DateTime? LastLoginAt { get; private set; }

public UserStatistics? Statistics { get; private set; }

public Subscription? Subscription { get; private set; }
    public ICollection<UserProgramProgress> ProgramProgresses { get; private set; }
    = new List<UserProgramProgress>();

public ICollection<UserVideoProgress> VideoProgresses { get; private set; }
    = new List<UserVideoProgress>();

    // Navigation
    public AuthIdentity? AuthIdentity { get; private set; }

    public ICollection<RefreshToken> RefreshTokens { get; private set; }
        = new List<RefreshToken>();

    private User()
    {
    }

    // ثبت‌نام کامل
    public User(
        string firstName,
        string lastName,
        string phoneNumber,
        string? email = null)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email;
    }

    // ورود با OTP
    public User(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        FirstName = string.Empty;
        LastName = string.Empty;
    }

    public void Verify()
    {
        IsVerified = true;
        SetUpdated();
    }

    public void Activate()
    {
        IsActive = true;
        SetUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdated();
    }

    public void SetLastLogin()
    {
        LastLoginAt = DateTime.UtcNow;
        SetUpdated();
    }

    public void ChangePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        IsVerified = false;
        SetUpdated();
    }

    public void UpdateProfile(
        string firstName,
        string lastName,
        string? email,
        string? avatarUrl,
        DateOnly? birthDate,
        Gender? gender,
        decimal? height,
        decimal? weight,
        FitnessGoal? goal,
        ActivityLevel? activityLevel)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AvatarUrl = avatarUrl;

        BirthDate = birthDate;
        Gender = gender;
        Height = height;
        Weight = weight;
        Goal = goal;
        ActivityLevel = activityLevel;

        SetUpdated();
    }
    public ICollection<Notification> Notifications { get; private set; }
    = new List<Notification>();
}
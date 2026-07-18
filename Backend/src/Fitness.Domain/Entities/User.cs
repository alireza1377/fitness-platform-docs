using Fitness.Domain.Common;

namespace Fitness.Domain.Entities;

public class User : AuditableEntity
{
    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public string Email { get; private set; } = string.Empty;

    public string PhoneNumber { get; private set; } = string.Empty;

    public bool IsActive { get; private set; } = true;

    public bool IsVerified { get; private set; } = false;

    // Navigation
   public AuthIdentity? AuthIdentity { get; private set; }
   public ICollection<RefreshToken> RefreshTokens { get; private set; }
    = new List<RefreshToken>();

    private User()
    {
    }

    public User(
        string firstName,
        string lastName,
        string phoneNumber,
        string? email = null)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;
        Email = email ?? string.Empty;
    }

    public void Verify()
    {
        IsVerified = true;
        SetUpdated();
    }

    public void Deactivate()
    {
        IsActive = false;
        SetUpdated();
    }

    public void Activate()
    {
        IsActive = true;
        SetUpdated();
    }

    public void UpdateProfile(
        string firstName,
        string lastName,
        string? email = null)
    {
        FirstName = firstName;
        LastName = lastName;

        if (!string.IsNullOrWhiteSpace(email))
            Email = email;

        SetUpdated();
    }

    public void ChangePhoneNumber(string phoneNumber)
    {
        PhoneNumber = phoneNumber;
        IsVerified = false;

        SetUpdated();
    }
}
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

    private User()
    {
    }

   public User(
    string firstName,
    string lastName,
    string phoneNumber)
{
    FirstName = firstName;
    LastName = lastName;
    PhoneNumber = phoneNumber;
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

    public void UpdateProfile(
        string firstName,
        string lastName,
        string? phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        PhoneNumber = phoneNumber;

        SetUpdated();
    }

    
}
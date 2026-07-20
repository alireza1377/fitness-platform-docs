using Fitness.Domain.Enums;

namespace Fitness.Application.DTOs.Profile;

public class UpdateProfileRequest
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? AvatarUrl { get; set; }

    public DateOnly? BirthDate { get; set; }

    public Gender? Gender { get; set; }

    public decimal? Height { get; set; }

    public decimal? Weight { get; set; }

    public FitnessGoal? Goal { get; set; }

    public ActivityLevel? ActivityLevel { get; set; }
}
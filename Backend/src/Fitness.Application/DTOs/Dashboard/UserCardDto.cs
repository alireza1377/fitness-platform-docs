namespace Fitness.Application.DTOs.Dashboard;

public class UserCardDto
{
    public Guid UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string? ProfileImageUrl { get; set; }

    public string? Level { get; set; }
}
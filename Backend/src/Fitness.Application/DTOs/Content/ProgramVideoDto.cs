namespace Fitness.Application.DTOs.Content;

public class ProgramVideoDto
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string VideoUrl { get; set; } = string.Empty;

    public TimeSpan Duration { get; set; }

    public int Order { get; set; }
}
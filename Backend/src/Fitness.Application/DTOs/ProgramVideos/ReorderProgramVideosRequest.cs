namespace Fitness.Application.DTOs.ProgramVideos;

public class ReorderProgramVideosRequest
{
    public Guid ProgramId { get; set; }

    public List<Guid> VideoIds { get; set; } = [];
}
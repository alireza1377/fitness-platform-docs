namespace Fitness.Application.DTOs.Programs;

public class ReorderProgramsRequest
{
    public Guid CategoryId { get; set; }

    public List<Guid> ProgramIds { get; set; } = [];
}
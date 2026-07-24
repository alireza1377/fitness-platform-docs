using Microsoft.AspNetCore.Http;

namespace Fitness.API.DTOs.Programs;

public class UploadProgramCoverRequest
{
    public Guid ProgramId { get; set; }

    public IFormFile Cover { get; set; } = default!;
}
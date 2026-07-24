using Fitness.Application.DTOs.ProgramVideos;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers.Admin;

[ApiController]
[Route("api/admin/program-videos")]
//[Authorize(Roles = "Admin")]
public class ProgramVideoController : ControllerBase
{
    private readonly IProgramVideoService _service;

    public ProgramVideoController(
        IProgramVideoService service)
    {
        _service = service;
    }

    [HttpGet("program/{programId:guid}")]
    public async Task<IActionResult> GetByProgram(
        Guid programId,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetByProgramAsync(
            programId,
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetByIdAsync(
            id,
            cancellationToken);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateProgramVideoRequest request,
        CancellationToken cancellationToken)
    {
        var id = await _service.CreateAsync(
            request,
            cancellationToken);

        return Ok(new
        {
            Id = id
        });
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateProgramVideoRequest request,
        CancellationToken cancellationToken)
    {
        await _service.UpdateAsync(
            id,
            request,
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _service.DeleteAsync(
            id,
            cancellationToken);

        return NoContent();
    }

    [HttpPut("reorder")]
public async Task<IActionResult> Reorder(
    ReorderProgramVideosRequest request,
    CancellationToken cancellationToken)
{
    await _service.ReorderAsync(
        request,
        cancellationToken);

    return NoContent();
}
}
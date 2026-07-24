using Fitness.Application.DTOs.Programs;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers.Admin;

[ApiController]
[Route("api/admin/programs")]
// [Authorize(Roles = "Admin")]
public class FitnessProgramController : ControllerBase
{
    private readonly IFitnessProgramService _service;

    public FitnessProgramController(
        IFitnessProgramService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        CancellationToken cancellationToken)
    {
        var result = await _service.GetAllAsync(
            cancellationToken);

        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
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

    [HttpGet("category/{categoryId:guid}")]
    public async Task<IActionResult> GetByCategory(
        Guid categoryId,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetByCategoryAsync(
            categoryId,
            cancellationToken);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateFitnessProgramRequest request,
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
        UpdateFitnessProgramRequest request,
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
    ReorderProgramsRequest request,
    CancellationToken cancellationToken)
{
    await _service.ReorderAsync(
        request,
        cancellationToken);

    return NoContent();
}

[HttpPost("upload-cover")]
public async Task<IActionResult> UploadCover(
    [FromForm] UploadProgramCoverRequest request,
    CancellationToken cancellationToken)
{
    if (request.Cover == null || request.Cover.Length == 0)
        return BadRequest("Cover is required.");

    await using var stream = request.Cover.OpenReadStream();

    await _service.UploadCoverAsync(
        request.ProgramId,
        stream,
        request.Cover.FileName,
        cancellationToken);

    return Ok();
}
}
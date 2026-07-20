using Fitness.Application.DTOs.Profile;
using Fitness.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Fitness.API.Controllers;

[ApiController]
[Route("api/profile")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(IProfileService profileService)
    {
        _profileService = profileService;
    }

    [HttpGet]
    public async Task<ActionResult<ProfileResponse>> GetProfile(
        CancellationToken cancellationToken)
    {
        var profile = await _profileService.GetProfileAsync(
            cancellationToken);

        return Ok(profile);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProfile(
        [FromBody] UpdateProfileRequest request,
        CancellationToken cancellationToken)
    {
        await _profileService.UpdateProfileAsync(
            request,
            cancellationToken);

        return NoContent();
    }
}
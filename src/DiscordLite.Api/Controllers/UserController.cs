using DiscordLite.Application.Users.GetMyProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscordLite.Api.Controllers;

[ApiController]
[Route("api/user")]
public sealed class UserController(ISender sender) : ControllerBase
{
    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<UserProfileResponse>> GetMyProfile(CancellationToken ct)
    {
        var result = await sender.Send(new GetMyProfileQuery(), ct);
        return Ok(result);  
    }
 

}
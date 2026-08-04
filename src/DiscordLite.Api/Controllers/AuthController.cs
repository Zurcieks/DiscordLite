using DiscordLite.Application.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiscordLite.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class AuthController(ISender sender): ControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<RegisterUserResponse>> Register(RegisterUserCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return Ok(result);
    }
}
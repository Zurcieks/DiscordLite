using DiscordLite.Application.Auth.Login;
using DiscordLite.Application.Auth.Logout;
using DiscordLite.Application.Auth.Refresh;
using DiscordLite.Application.Auth.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DiscordLite.Api.Controllers;

 
[ApiController]
[Route("api/auth")]
public sealed class AuthController(ISender sender) : ControllerBase
{
    private const string RefreshTokenCookieName = "refreshToken";

    [HttpPost("register")]
    public async Task<ActionResult<RegisterUserResponse>> Register(RegisterUserCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginUserResponse>> Login(LoginUserCommand command, CancellationToken ct)
    {
        var result = await sender.Send(command, ct);
        
        
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<RefreshTokenResponse>> Refresh(CancellationToken ct)
    {
        var refreshToken = Request.Cookies[RefreshTokenCookieName];

        if (string.IsNullOrWhiteSpace(refreshToken))
            return Unauthorized();

        var result = await sender.Send(new RefreshTokenCommand(refreshToken), ct);
        

        return Ok(result);
    }

    [HttpPost("logout")]
    public async Task<ActionResult> Logout(CancellationToken ct)
    {
        var refreshToken = Request.Cookies[RefreshTokenCookieName];
        
        if (string.IsNullOrWhiteSpace(refreshToken))
            return NoContent();
        
        await sender.Send(new LogoutCommand(refreshToken), ct);
        
        
        return NoContent();
    }

}
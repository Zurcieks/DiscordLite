using DiscordLite.Application.Friendships.AcceptFriend;
using DiscordLite.Application.Friendships.AddFriend;
using DiscordLite.Application.Friendships.GetFriendsRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DiscordLite.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public sealed class FriendshipController(ISender sender) : ControllerBase
    {
        [HttpPost("requests")]
        public async Task<ActionResult<string>> AddFriend(AddFriendCommand command, CancellationToken ct)
        {
            return Ok(await sender.Send(command, ct));
        }

        [HttpGet("requests")]
        public async Task<ActionResult<GetFriendsRequestsResponse>> GetFriendRequests(CancellationToken ct)
        {
            return Ok(await sender.Send(new GetFriendsRequestQuery(), ct));
        }

        [HttpPost("requests/{friendshipId:guid}/accept")]
        public async Task<ActionResult<string>> AcceptFriend(
         [FromRoute] Guid friendshipId,
         CancellationToken ct)
        {
            var command = new AcceptFriendCommand(friendshipId);

            var result = await sender.Send(command, ct);

            return Ok(result);
        }
    }
}

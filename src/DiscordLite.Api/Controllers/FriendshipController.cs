using DiscordLite.Application.Friendships.AcceptFriend;
using DiscordLite.Application.Friendships.AddFriend;
using DiscordLite.Application.Friendships.CancelFriendRequest;
using DiscordLite.Application.Friendships.GetAllFriends;
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
        [HttpPost]
        public async Task<ActionResult<string>> AddFriend(
            AddFriendCommand command,
            CancellationToken ct)
        {
            return Ok(await sender.Send(command, ct));
        }

        [HttpGet("requests")]
        public async Task<ActionResult<GetFriendsRequestsResponse>> GetFriendRequests(
            CancellationToken ct)
        {
            return Ok(await sender.Send(new GetFriendsRequestQuery(), ct));
        }

        [HttpPost("requests/{friendshipId:guid}/accept")]
        public async Task<ActionResult> AcceptFriend(
            [FromRoute] Guid friendshipId,
            CancellationToken ct)
        {
            await sender.Send(new AcceptFriendCommand(friendshipId), ct);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<GetAllFriendResponse>> GetAllFriends(
            CancellationToken ct)
        {
            return Ok(await sender.Send(new GetAllFriendsQuery(), ct));
        }

        [HttpPost("requests/{friendshipId}/cancel")]
        public async Task<IActionResult> CancelFriend(
            [FromRoute] Guid friendshipId,
            CancellationToken ct)
        {
            await sender.Send(new CancelFriendRequestCommand(friendshipId), ct);

            return NoContent();
        }
    }
}

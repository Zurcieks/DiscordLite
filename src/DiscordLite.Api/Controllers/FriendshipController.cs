using DiscordLite.Application.Friendships.AcceptFriend;
using DiscordLite.Application.Friendships.AddFriend;
using DiscordLite.Application.Friendships.CancelFriendRequest;
using DiscordLite.Application.Friendships.DeleteFriend;
using DiscordLite.Application.Friendships.GetAllFriends;
using DiscordLite.Application.Friendships.GetFriendsRequest;
using DiscordLite.Application.Friendships.RejectFriendRequest;
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

        [HttpPost("requests/{friendshipId:guid}/cancel")]
        public async Task<ActionResult> CancelFriend(
            [FromRoute] Guid friendshipId,
            CancellationToken ct)
        {
            await sender.Send(new CancelFriendRequestCommand(friendshipId), ct);

            return NoContent();
        }

        [HttpPost("requests/{friendshipId:guid}/reject")]
        public async Task<ActionResult> RejectFriend(
            [FromRoute] Guid friendshipId,
            CancellationToken ct)
        {
            await sender.Send(new RejectFriendRequestCommand(friendshipId), ct);

            return NoContent();
        }

        [HttpDelete("{friendshipId:guid}")]
        public async Task<ActionResult> DeleteFriend(
            [FromRoute] Guid friendshipId,
            CancellationToken ct)
        {
            await sender.Send(new DeleteFriendCommand(friendshipId), ct);

            return NoContent();
        }
    }
}

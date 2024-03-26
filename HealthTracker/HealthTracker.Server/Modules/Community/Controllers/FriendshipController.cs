using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace HealthTracker.Server.Modules.Community.Controllers
{

    //Pomysł zmiany z freindship na relations - wtedy można będzie lepiej obsługiwać takie elementy jak usunięty/ zablokowany
    [Route("api")]
    [ApiController]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendRepository _friendRepository;
        public FriendshipController(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        [HttpGet("users/{userId}/friends")]
        public async Task<ActionResult<List<FriendDTO>>> GetFriendList(int userId)
        {
            try
            {
                var friendsListDto = await _friendRepository.GetFriendList(userId);
                if (friendsListDto == null || friendsListDto.Count == 0)
                {
                    return NotFound("Freinds not found.");
                }
                return Ok(friendsListDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("users/{userId}/friends/{friendId}")]
        public async Task<ActionResult> FriendshipRequest(int userId, int friendId)
        {
            try
            {
                await _friendRepository.CreateFriendshipRequest(userId, friendId);
                return Created();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("users/{userId}/friends/{friendId}/status")]
        public async Task<ActionResult> ChangeFriendshipStatus(int userId, int friendId, [FromQuery] bool isAccepted)
        {
            try
            {
                await _friendRepository.ChangeFriendshipStatus(userId, friendId, isAccepted);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpDelete("users/{userId}/friends/{friendId}")]
        public async Task<ActionResult> DeleteFriendship(int userId, int friendId)
        {
            try
            {
                await _friendRepository.DeleteFriendship(userId, friendId);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}

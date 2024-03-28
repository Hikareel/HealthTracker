using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
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

        [HttpPost("users/friends/")]
        public async Task<ActionResult> FriendshipCreate([FromBody] CreateFriendshipDTO createFriendshipDTO)
        {
            try
            {
                var result = await _friendRepository.CreateFriendshipRequest(createFriendshipDTO);
                
                if (result != null)
                {
                    return CreatedAtAction(nameof(GetFriendship), new { friendshipId = result.Id }, result);
                }
                else
                {
                    return BadRequest("Friend couldn't be created because the user or friend does not exist.");
                }
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/friends/{friendshipId}")]
        public async Task<ActionResult<FriendshipDTO>> GetFriendship(int friendshipId)
        {
            try
            {
                var friendship = await _friendRepository.GetFriendship(friendshipId);
                if (friendship == null)
                {
                    return NotFound("Friend not found.");
                }
                return Ok(friendship);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/{userId}/friends")]
        public async Task<ActionResult<List<FriendDTO>>> GetFriendList(int userId)
        {
            try
            {
                var friendsListDto = await _friendRepository.GetFriendList(userId);
                if (friendsListDto == null || friendsListDto.Count == 0)
                {
                    return Ok("Friends not found.");
                }
                return Ok(friendsListDto);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut("users/{userId}/friends/{friendId}/accept")]
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

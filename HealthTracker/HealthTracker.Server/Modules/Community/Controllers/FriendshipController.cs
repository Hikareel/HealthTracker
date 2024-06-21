using HealthTracker.Server.Core.Exceptions;
using HealthTracker.Server.Core.Exceptions.Community;
using HealthTracker.Server.Modules.Community.DTOs;
using HealthTracker.Server.Modules.Community.Models;
using HealthTracker.Server.Modules.Community.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthTracker.Server.Modules.Community.Controllers
{

    [Route("api")]
    [ApiController]
    [Authorize]
    public class FriendshipController : ControllerBase
    {
        private readonly IFriendRepository _friendRepository;
        private readonly ILogger<FriendshipController> _logger;
        public FriendshipController(IFriendRepository friendRepository, ILogger<FriendshipController> logger)
        {
            _friendRepository = friendRepository;
            _logger = logger;
        }

        [HttpPost("users/friends/")]
        public async Task<ActionResult> FriendshipCreate([FromBody] CreateFriendshipDTO createFriendshipDTO)
        {
            try
            {
                var result = await _friendRepository.CreateFriendshipRequest(createFriendshipDTO);
                return CreatedAtAction(nameof(GetFriendship), new { friendshipId = result.Id }, result);

            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error occurred during the create friendship process for {DTO}.", createFriendshipDTO);
                return BadRequest(ex.InnerException?.Message ?? "Database error.");
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (FriendshipAlreadyExistsException ex)
            {
                return StatusCode(409, ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the create friendship process for {DTO}.", createFriendshipDTO);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/friends/{friendshipId}")]
        public async Task<ActionResult<FriendshipDTO>> GetFriendship(int friendshipId)
        {
            try
            {
                var friendship = await _friendRepository.GetFriendship(friendshipId);
                return Ok(friendship);
            }
            catch(FriendshipNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get friendship process for {FriendshipId}.", friendshipId);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("users/{userId}/friends")]
        public async Task<ActionResult<List<FriendDTO>>> GetFriendList(int userId)
        {
            try
            {
                var friendsListDto = await _friendRepository.GetFriendList(userId);
                return Ok(friendsListDto);
            }
            catch (UserNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the get friendships list process for {UserId}.", userId);
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
            catch (DbUpdateException ex)
            {
                return BadRequest(ex.InnerException?.Message ?? "Database error.");
            }
            catch (Exception ex) when (ex is UserNotFoundException || ex is FriendshipNotFoundException)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the change friendship status process for user {UserId} to friend {FriendId}.", userId, friendId);
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
            catch(FriendshipNotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the delete friendship process for user {UserId} to friend {FriendId}.", userId, friendId);
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
